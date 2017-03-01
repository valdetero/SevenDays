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
using Xamarin.Forms;
using SevenDays.Core.Logging;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class PlayerViewModel : ViewModelBase, IViewModel
    {
        private readonly ISevendayService _sevendayService;
		private readonly ILogger _logger;
        public PlayerViewModel()
        {
            _sevendayService = Container.Resolve<ISevendayService>();
			_logger = Container.Resolve<ILogger>();

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
        public ImageSource Avatar => string.IsNullOrEmpty(avatar) ? null : ImageSource.FromUri(new Uri(avatar));


        public ObservableCollection<Grouping<string, InventoryViewModel>> Inventory { get; set; }

        private RelayCommand _getLoadInventoryCommand;
        public ICommand LoadInventoryCommand
        {
            get { return _getLoadInventoryCommand ?? (_getLoadInventoryCommand = new RelayCommand(async () => await ExecuteLoadInventoryCommand())); }
        }

        [Track]
        public async Task ExecuteLoadInventoryCommand()
        {
            Inventory.Clear();

            Inventory inventory;

            _logger.Track($"Getting player inventory for {SteamId}");
            using (TrackTime("Seven_GetPlayerInventory"))
            {
                var invResponse = await _sevendayService.GetPlayerInventory(SteamId);

                if (!invResponse.Successful)
                    return;

                inventory = invResponse.Result;
            }

			_logger.Track($"Found {inventory.Bag.Count()} inventory items in bag");
            _logger.Track($"Found {inventory.Belt.Count()} inventory items in belt");

            var inventories =   inventory.Bag.Where(x => x != null && x.Count > 0).Select(x => new InventoryViewModel(x, "Bag")).Concat(
                                inventory.Belt.Where(x => x != null && x.Count > 0).Select(x => new InventoryViewModel(x, "Belt"))).Concat(
								inventory.Equipment.ToItemList().Select(x => new InventoryViewModel(x, "Equipment")))
								.ToList();

            foreach (var inv in inventories)
            {
                await inv.Init();
            }

            var allItems = (from i in inventories
                           group i by i.Pack into Group
                           select new Grouping<string, InventoryViewModel>(Group.Key, Group)).ToList();

            Inventory = new ObservableCollection<Grouping<string, InventoryViewModel>>(allItems);

			_logger.Track($"Added {allItems.Count} inventory items to view");
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
