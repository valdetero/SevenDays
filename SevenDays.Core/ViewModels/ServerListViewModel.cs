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
using Xamarin;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ServerListViewModel : IViewModel
    {
        private ISevendayService sevendayService;
        private ICacheService cache;
        public ServerListViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
            cache = Container.Resolve<ICacheService>();

            Servers = new ObservableCollection<ServerViewModel>();
        }

        public ObservableCollection<ServerViewModel> Servers { get; set; }

        private RelayCommand getServersCommand;
        public ICommand GetServersCommand
        {
            get { return getServersCommand ?? (getServersCommand = new RelayCommand(async () => await ExecuteGetServersCommand())); }
        }

        [Insights]
        public async Task ExecuteGetServersCommand()
        {
            Servers.Clear();

            var entities = await cache.GetAllObjects<Model.Entity.Server>();

            var servers = entities.Select(x => new ServerViewModel(x)).ToList();

            Insights.Track(string.Format("Loaded {0} servers from cache", servers.Count));

            Servers = new ObservableCollection<ServerViewModel>(servers);
        }

        private RelayCommand checkConnectivityCommand;
        public ICommand CheckConnectivityCommand
        {
            get { return checkConnectivityCommand ?? (checkConnectivityCommand = new RelayCommand(async () => await ExecuteCheckConnectivityCommand())); }
        }

        [Insights]
        public async Task<bool> ExecuteCheckConnectivityCommand()
        {
            throw new NotImplementedException();
        }

        private RelayCommand deleteServerCommand;
        public ICommand DeleteServerCommand
        {
            get { return deleteServerCommand ?? (deleteServerCommand = new RelayCommand(async () => await ExecuteDeleteServerCommand())); }
        }

        [Insights]
        public async Task<bool> ExecuteDeleteServerCommand()
        {
            throw new NotImplementedException();
        }

        private RelayCommand addServerCommand;
        public ICommand AddServerCommand
        {
            get { return addServerCommand ?? (addServerCommand = new RelayCommand(async () => await ExecuteAddServerCommand())); }
        }

        [Insights]
        public async Task<bool> ExecuteAddServerCommand()
        {
            throw new NotImplementedException();
        }
    }
}
