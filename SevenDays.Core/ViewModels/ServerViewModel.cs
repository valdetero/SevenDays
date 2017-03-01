using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Akavache;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using SevenDays.Model.Entity;
using SevenDays.Core.Logging;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ServerViewModel : IViewModel
    {
        private readonly ISevendayService sevendayService;
        private readonly ICacheService cache;
        private readonly ISettings settings;
        public ServerViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
            cache = Container.Resolve<ICacheService>();
            settings = Container.Resolve<ISettings>();
        }

        public ServerViewModel(Server server) : this()
        {
            _existingHost = Host = server.Host;
            _existingPort = Port = server.Port;

            IsFavorite = settings.SevendaysSelectedServer == server.ToString();
        }

        private string _existingHost { get; set; }
        private string _existingPort { get; set; }

        public string Host { get; set; }
        public string Port { get; set; }
        public bool? IsReachable { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsNotFavorite { get { return !this.IsFavorite; } }
		//this doesn't work on Mac/Xamarin Studio
		public bool CanSave { get { return true; }}//{ get { return !string.IsNullOrEmpty(this.Host) && !string.IsNullOrEmpty(this.Port); } }
        public bool CanDelete { get { return !string.IsNullOrEmpty(this._existingHost) && !string.IsNullOrEmpty(this._existingPort); } }

        private RelayCommand getSaveCommand;
        public ICommand SaveCommand
        {
            get { return getSaveCommand ?? (getSaveCommand = new RelayCommand(async () => await ExecuteSaveCommand())); }
        }

        [Track]
        public async Task<bool> ExecuteSaveCommand()
        {
            int port;
            int.TryParse(Port, out port);

            await deletingExistingItem();

			var server = setDefault();

            if (await IsExistingItem())
                return true;

            return await cache.InsertObject(server.ToString(), server);
        }

        private RelayCommand getSetDefaultCommand;
        public ICommand SetDefaultCommand
        {
            get { return getSetDefaultCommand ?? (getSetDefaultCommand = new RelayCommand(() => ExecuteSetDefaultCommand())); }
        }

        [Track]
        public void ExecuteSetDefaultCommand()
        {
            setDefault();
        }

        private RelayCommand getCheckConnectivityCommand;
        public ICommand CheckConnectivityCommand
        {
            get { return getCheckConnectivityCommand ?? (getCheckConnectivityCommand = new RelayCommand(async () => await ExecuteCheckConnectivityCommand())); }
        }

        [Track]
        public async Task<bool> ExecuteCheckConnectivityCommand()
        {
            IsReachable = await sevendayService.CanConnectToServer(Host, Port);

            return IsReachable.Value;
        }

        private RelayCommand getDeleteCommand;
        public ICommand DeleteCommand
        {
            get { return getDeleteCommand ?? (getDeleteCommand = new RelayCommand(async () => await ExecuteDeleteCommand())); }
        }

        [Track]
        public async Task ExecuteDeleteCommand()
        {
            await deletingExistingItem();
        }

        private Task deletingExistingItem()
        {
            var keyToRemove = new Server(_existingHost, _existingPort).ToString();

            return cache.RemoveObject(keyToRemove);
        }

        private async Task<bool> IsExistingItem()
        {
            var keyToGet = new Server(Host, Port).ToString();

            return await cache.GetObject<Server>(keyToGet) != null;
        }

        private Server setDefault()
        {
            var server = new Server(Host, Port);

            settings.SevendaysSelectedServer = server.ToString();

            return server;
        }
    }
}
