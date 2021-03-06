﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using Xamarin;
using SevenDays.Core.Logging;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ServerListViewModel : IViewModel
    {
        private readonly ISevendayService sevendayService;
        private readonly ICacheService cache;
        private readonly ISettings settings;
		private readonly ILogger logger;
        public ServerListViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
            cache = Container.Resolve<ICacheService>();
            settings = Container.Resolve<ISettings>();
			logger = Container.Resolve<ILogger>();

            Servers = new ObservableCollection<ServerViewModel>();
        }

        public ObservableCollection<ServerViewModel> Servers { get; set; }

        private RelayCommand getServersCommand;
        public ICommand GetServersCommand
        {
            get { return getServersCommand ?? (getServersCommand = new RelayCommand(async () => await ExecuteGetServersCommand())); }
        }

        [Track]
        public async Task ExecuteGetServersCommand()
        {
            Servers.Clear();

            var entities = await cache.GetAllObjects<Model.Entity.Server>();

#if DEBUG
            if (!entities.Any())
            {
                var data = InitialData.GetServers();

                foreach (var item in data)
                {
                    await cache.InsertObject(item.ToString(), item);
                }

                settings.SevendaysSelectedServer = data.First().ToString();

                entities = await cache.GetAllObjects<Model.Entity.Server>();
            }
#endif

            var servers = entities.Select(x => new ServerViewModel(x)).ToList();

            logger.Track(string.Format("Loaded {0} servers from cache", servers.Count));

            Servers = new ObservableCollection<ServerViewModel>(servers);
        }
    }
}
