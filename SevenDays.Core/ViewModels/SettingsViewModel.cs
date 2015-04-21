using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class SettingsViewModel : IViewModel
    {
        private ISevendayService sevendayService;
        public SettingsViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();

            Server = Settings.SevendaysServer;
            Port = Settings.SevendaysPort;
        }

        public string Server { get; set; }
        public string Port { get; set; }

        private RelayCommand getSaveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get { return getSaveSettingsCommand ?? (getSaveSettingsCommand = new RelayCommand(async () => await ExecuteSaveSettingsCommand())); }
        }

        [Insights]
        public async Task ExecuteSaveSettingsCommand()
        {
            int port;
            int.TryParse(Port, out port);

            Settings.SevendaysServer = Server;
            Settings.SevendaysPort = port.ToString();
        }

        private RelayCommand getCheckConnectivityCommand;
        public ICommand CheckConnectivityCommand
        {
            get { return getCheckConnectivityCommand ?? (getCheckConnectivityCommand = new RelayCommand(async () => await ExecuteCheckConnectivityCommand())); }
        }

        [Insights]
        public async Task<bool> ExecuteCheckConnectivityCommand()
        {
            return await sevendayService.CanConnectToServer();
        }
    }
}
