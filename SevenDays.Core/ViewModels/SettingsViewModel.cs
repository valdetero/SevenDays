using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using SevenDays.Core.Logging;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class SettingsViewModel : IViewModel
    {
        private readonly ISevendayService sevendayService;
        private readonly ISettings settings;
        public SettingsViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
            settings = Container.Resolve<ISettings>();

            Server = settings.SevendaysSelectedServer.Split(':')[0];
            Port = settings.SevendaysSelectedServer.Split(':')[1];
        }

        public string Server { get; set; }
        public string Port { get; set; }

        private RelayCommand getSaveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get { return getSaveSettingsCommand ?? (getSaveSettingsCommand = new RelayCommand(() => ExecuteSaveSettingsCommand())); }
        }

        [Track]
        public void ExecuteSaveSettingsCommand()
        {
            int port;
            int.TryParse(Port, out port);

            settings.SevendaysSelectedServer = string.Format("{0}:{1}", Server, Port);
        }

        private RelayCommand getCheckConnectivityCommand;
        public ICommand CheckConnectivityCommand
        {
            get { return getCheckConnectivityCommand ?? (getCheckConnectivityCommand = new RelayCommand(async () => await ExecuteCheckConnectivityCommand())); }
        }

        [Track]
        public async Task<bool> ExecuteCheckConnectivityCommand()
        {
            return await sevendayService.CanConnectToServer();
        }
    }
}
