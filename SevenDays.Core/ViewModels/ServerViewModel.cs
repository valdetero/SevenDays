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

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ServerViewModel : IViewModel
    {
        private ISevendayService sevendayService;
        private ICacheService cache;
        public ServerViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
            cache = Container.Resolve<ICacheService>();
        }

        public ServerViewModel(Server server) : this()
        {
            _existingHost = Host = server.Host;
            _existingPort = Port = server.Port;
        }

        private string _existingHost { get; set; }
        private string _existingPort { get; set; }

        public string Host { get; set; }
        public string Port { get; set; }
        public bool? IsReachable { get; set; }

        private RelayCommand getSaveCommand;
        public ICommand SaveCommand
        {
            get { return getSaveCommand ?? (getSaveCommand = new RelayCommand(async () => await ExecuteSaveCommand())); }
        }

        [Insights]
        public async Task<bool> ExecuteSaveCommand()
        {
            int port;
            int.TryParse(Port, out port);
                       
            await deletingExistingItem();

            var server = setDefault();

            return await cache.InsertObject(server.ToString(), server);
        }

        private RelayCommand getSetDefaultCommand;
        public ICommand SetDefaultCommand
        {
            get { return getSetDefaultCommand ?? (getSetDefaultCommand = new RelayCommand(() => ExecuteSetDefaultCommand())); }
        }

        [Insights]
        public void ExecuteSetDefaultCommand()
        {
            setDefault();
        }

        private RelayCommand getCheckConnectivityCommand;
        public ICommand CheckConnectivityCommand
        {
            get { return getCheckConnectivityCommand ?? (getCheckConnectivityCommand = new RelayCommand(async () => await ExecuteCheckConnectivityCommand())); }
        }

        [Insights]
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

        [Insights]
        public async Task ExecuteDeleteCommand()
        {
            await deletingExistingItem();
        }

        private Task deletingExistingItem()
        {
            var keyToRemove = new Server(_existingHost, _existingPort).ToString();

            return cache.RemoveObject(keyToRemove);
        }

        private Server setDefault()
        {
            var server = new Server(Host, Port);

            Settings.SevendaysSelectedServer = server.ToString();

            return server;
        }
    }
}
