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
using SevenDays.Model.Base;
using SevenDays.Model.Mapper;
using SevenDays.Core.Logging;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class PlayerListViewModel : ViewModelBase, IViewModel
    {
        private ISevendayService sevendayService;
        private ISteamService steamService;
        private ICacheService cache;
		private ILogger logger;
        public PlayerListViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
            steamService = Container.Resolve<ISteamService>();
            cache = Container.Resolve<ICacheService>();
			logger = Container.Resolve<ILogger>();

            Players = new ObservableCollection<PlayerViewModel>();
            CanLoadMore = true;
        }

        public ObservableCollection<PlayerViewModel> Players { get; set; }

        private RelayCommand getPlayerSummariesCommand;
        public ICommand GetPlayerSummariesCommand
        {
            get { return getPlayerSummariesCommand ?? (getPlayerSummariesCommand = new RelayCommand(async () => await ExecuteGetPlayerSummariesCommand())); }
        }

        [Track]
        public async Task ExecuteGetPlayerSummariesCommand()
        {
            Players.Clear();

            ListResponse<Model.Seven.Player> locationResponse;
            using (TrackTime("Seven_GetPlayersLocation"))
                locationResponse = await sevendayService.GetPlayersLocation();

            if (!locationResponse.Successful)
                    return;

            var sevens = locationResponse.Result.ToList();

			logger.Track($"Found {sevens.Count} seven players");

            ListResponse<Model.Steam.Player> summaryResponse;
            using (TrackTime("Steam_GetPlayerSummaries"))
                summaryResponse = await steamService.GetPlayerSummaries(sevens.Select(x => x.SteamId).ToArray());

            if (!summaryResponse.Successful)
                return;

            var steams = summaryResponse.Result.ToList();

			logger.Track($"Found {steams.Count} steam players");

            addPlayers(sevens, steams);

			logger.Track($"Added {Players.Count} players to view");
        }

        private RelayCommand loadMoreCommand;
        public ICommand LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new RelayCommand(async () => await ExecuteLoadMoreCommand())); }
        }

        [Track]
        public async Task ExecuteLoadMoreCommand()
        {
            if (!CanLoadMore)
                return;

            IsBusy = true;

            ListResponse<Model.Seven.Player> locationResponse;
            using (TrackTime("Seven_GetPlayersLocation"))
                locationResponse = await sevendayService.GetPlayersLocation();

            if (!locationResponse.Successful)
                    return;

            var sevens = locationResponse.Result
                .OrderByDescending(x => x.IsOnline)
                .Skip(Players.Count)
                .Take(Settings.PageSize)
                .ToList();

			logger.Track($"Found {sevens.Count} seven players");

            ListResponse<Model.Steam.Player> summaryResponse;
            using (TrackTime("Steam_GetPlayerSummaries"))
                summaryResponse = await steamService.GetPlayerSummaries(sevens.Select(x => x.SteamId).ToArray());

            if (!summaryResponse.Successful)
                return;

            var steams = summaryResponse.Result.ToList();

			logger.Track($"Found {steams.Count} steam players");

            addPlayers(sevens, steams);

			logger.Track($"Added {Players.Count} players to view");

            CanLoadMore = sevens.Count == Settings.PageSize;

            IsBusy = false;
        }

        private RelayCommand isServerReachableCommand;
        public ICommand IsServerReachableCommand
        {
            get { return isServerReachableCommand ?? (isServerReachableCommand = new RelayCommand(async () => await ExecuteIsServerReachableCommand())); }
        }

        [Track]
        public async Task<bool> ExecuteIsServerReachableCommand()
        {
            return await sevendayService.CanConnectToServer();
        }

        private RelayCommand getCachedPlayersCommand;
        public ICommand GetCachedPlayersCommand
        {
            get { return getCachedPlayersCommand ?? (getCachedPlayersCommand = new RelayCommand(async () => await ExecuteGetCachedPlayersCommand())); }
        }

        [Track]
        public async Task ExecuteGetCachedPlayersCommand()
        {
            var sevens = await cache.GetObject<List<Model.Seven.Player>>("seven_players");
            var steams = await cache.GetObject<List<Model.Steam.Player>>("steam_players");

            if (sevens == null || steams == null)
                return;

            addPlayers(sevens, steams);
        }

        private void addPlayers(IEnumerable<Model.Seven.Player> sevens, IEnumerable<Model.Steam.Player> steams)
        {
            var players = sevens.Join(steams, _7 => _7.SteamId, s => s.SteamId, (_7, s) => PlayerMapper.Map(s, _7)).Select(x => new PlayerViewModel(x)).ToList();

            foreach (var player in players)
            {
                if (Players.Contains(player))
                    Players.Remove(player);

                Players.Add(player);
            }
        }

    }
}
