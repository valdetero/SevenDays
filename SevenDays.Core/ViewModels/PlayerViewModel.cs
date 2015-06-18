using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using SevenDays.Model.Seven;
using Xamarin;
using Xamarin.Forms;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class PlayerViewModel : IViewModel
    {
        private readonly ISevendayService _sevendayService;
        public PlayerViewModel()
        {
            _sevendayService = Container.Resolve<ISevendayService>();

            Inventory = new ObservableCollection<Grouping<string, InventoryViewModel>>();
        }

        public PlayerViewModel(Model.Player player):this()
        {
            Name = player.Name;
            IsOnline = player.IsOnline;
            avatar = player.Avatar;
            SteamAvailability = player.SteamAvailability;
            SteamId = player.SteamId;
            LastLogOff = player.LastLogOff.ToLocalTime().ToString();
        }

        public long SteamId { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public string SteamAvailability { get; set; }
        public Color SteamColor { get { return getColorFromState(); } }
        public bool ShouldShowAsOnline { get { return IsOnline == true; } }
        public bool ShouldShowAsOffline { get { return IsOnline == false; } }
        public string LastLogOff { get; set; }
        private string avatar;
        public ImageSource Avatar
        {
            get
            {
                if (string.IsNullOrEmpty(avatar))
                    return null;

                return Device.OnPlatform(
                    UriImageSource.FromUri(new Uri(avatar)),
                    UriImageSource.FromUri(new Uri(avatar)),
                    UriImageSource.FromUri(new Uri(avatar))
                );
            }
        }

        public ObservableCollection<Grouping<string, InventoryViewModel>> Inventory { get; set; }

        private RelayCommand _getLoadInventoryCommand;
        public ICommand LoadInventoryCommand
        {
            get { return _getLoadInventoryCommand ?? (_getLoadInventoryCommand = new RelayCommand(async () => await ExecuteLoadInventoryCommand())); }
        }

        [Insights]
        public async Task ExecuteLoadInventoryCommand()
        {
            Inventory.Clear();

            Inventory inventory;

            Insights.Track(string.Format("Getting player inventory for {0}", SteamId));
            using (var handle = Insights.TrackTime("Seven_GetPlayerInventory"))
            {
                var invResponse = await _sevendayService.GetPlayerInventory(SteamId);

                if (!invResponse.Successful)
                    return;

                inventory = invResponse.Result;
            }

            Insights.Track(string.Format("Found {0} inventory items in bag", inventory.Bag.Count()));
            Insights.Track(string.Format("Found {0} inventory items in belt", inventory.Belt.Count()));

            var inventories =   inventory.Bag.Where(x => x.Count > 0).Select(x => new InventoryViewModel(x, "Bag")).Concat(
                                inventory.Belt.Where(x => x.Count > 0).Select(x => new InventoryViewModel(x, "Belt"))).ToList();

            foreach (var inv in inventories)
            {
                await inv.Init();
            }

            var allItems = (from i in inventories
                           group i by i.Pack into Group
                           select new Grouping<string, InventoryViewModel>(Group.Key, Group)).ToList();

            Inventory = new ObservableCollection<Grouping<string, InventoryViewModel>>(allItems);

            Insights.Track(string.Format("Added {0} inventory items to view", allItems.Count));
        }

        private Color getColorFromState()
        {
            var val = (Model.Steam.PersonaState)Enum.Parse(typeof(Model.Steam.PersonaState), SteamAvailability);

            switch (val)
            {
                case Model.Steam.PersonaState.Offline:
                    return Color.FromHex("#666");
                case Model.Steam.PersonaState.Private:
                case Model.Steam.PersonaState.Busy:
                    return Color.Red;
                case Model.Steam.PersonaState.Snooze:
                case Model.Steam.PersonaState.Away:
                    return Color.Yellow.Darker();
                case Model.Steam.PersonaState.Online:
                default:
                    return Color.Green;
            }
        }
    }
}
