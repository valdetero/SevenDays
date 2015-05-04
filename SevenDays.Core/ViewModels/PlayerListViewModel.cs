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
        private ICacheService cache;
        public PlayerListViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
            steamService = Container.Resolve<ISteamService>();
            cache = Container.Resolve<ICacheService>();

            Players = new ObservableCollection<PlayerViewModel>();
        }

        public ObservableCollection<PlayerViewModel> Players { get; set; }

        private void addPlayers(IEnumerable<Model.Seven.Player> sevens, IEnumerable<Model.Steam.Player> steams)
        {
            var players = sevens.Join(steams, _7 => _7.SteamId, s => s.SteamId, (_7, s) => PlayerMapper.Map(s, _7)).Select(x => new PlayerViewModel(x)).ToList();

            Players = new ObservableCollection<PlayerViewModel>(players);
        }

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

            await cache.InsertObject("seven_players", sevens);

            Insights.Track(string.Format("Found {0} seven players", sevens.Count));

            var summaryResponse = await steamService.GetPlayerSummaries(sevens.Select(x => x.SteamId).ToArray());

            if (!summaryResponse.Successful)
                return;

            var steams = summaryResponse.Result.ToList();

            await cache.InsertObject("steam_players", steams);

            Insights.Track(string.Format("Found {0} steam players", steams.Count));

            addPlayers(sevens, steams);

            Insights.Track(string.Format("Added {0} players to view", players.Count));
        }

        private RelayCommand getIsServerReachableCommand;
        public ICommand IsServerReachableCommand
        {
            get { return getIsServerReachableCommand ?? (getIsServerReachableCommand = new RelayCommand(async () => await ExecuteIsServerReachableCommand())); }
        }

        [Insights]
        public async Task<bool> ExecuteIsServerReachableCommand()
        {
            return await sevendayService.CanConnectToServer();
        }

        private RelayCommand geGetCachedPlayersCommand;
        public ICommand GetCachedPlayersCommand
        {
            get { return getGetCachedPlayers ?? (getGetCachedPlayersCommand = new RelayCommand(async () => await ExecuteGetCachedPlayersCommand())); }
        }

        [Insights]
        public async Task ExecuteGetCachedPlayersCommand()
        {
            var sevens = await cache.GetObject<IEnumerable<Model.Seven.Player>>("seven_players");
            var steams = await cache.GetObject<IEnumerable<Model.Steam.Player>>("steam_players");

            if (sevens == null || steams == null)
                return;

            addPlayers(sevens, steams);
        }
    }
}
