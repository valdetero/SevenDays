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
using SevenDays.Model;
using SevenDays.Model.Mapper;
using Xamarin;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class PlayerListViewModel : IViewModel
    {
        private ISevendayService sevendayService;
        private ISteamService steamService;
        public PlayerListViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
            steamService = Container.Resolve<ISteamService>();

            Players = new ObservableCollection<PlayerViewModel>();
        }

        public ObservableCollection<PlayerViewModel> Players { get; set; }

        private RelayCommand getPlayerSummariesCommand;
        public ICommand GetPlayerSummariesCommand
        {
            get { return getPlayerSummariesCommand ?? (getPlayerSummariesCommand = new RelayCommand(async () => await ExecuteGetPlayerSummariesCommand())); }
        }

        [Insights]
        public async Task ExecuteGetPlayerSummariesCommand()
        {
            Players.Clear();

            var locationResponse = await sevendayService.GetPlayersLocation();

            if (!locationResponse.Successful)
                return;

            var sevens = locationResponse.Result.ToList();

            Insights.Track(string.Format("Found {0} seven players", sevens.Count));

            var summaryResponse = await steamService.GetPlayerSummaries(sevens.Select(x => x.SteamId).ToArray());

            if (!summaryResponse.Successful)
                return;

            var steams = summaryResponse.Result.ToList();

            Insights.Track(string.Format("Found {0} steam players", steams.Count));

            var players = sevens.Join(steams, _7 => _7.SteamId, s => s.SteamId, (_7, s) => PlayerMapper.Map(s, _7)).Select(x => new PlayerViewModel(x)).ToList();

            Players = new ObservableCollection<PlayerViewModel>(players);

            Insights.Track(string.Format("Added {0} players to view", players.Count));
        }
    }
}
