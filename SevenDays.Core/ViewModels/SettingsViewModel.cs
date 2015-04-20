using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class SettingsViewModel : IViewModel
    {
        public SettingsViewModel()
        {
            Server = Settings.SevendaysServer;
            Port = Settings.SevendaysPort;
        }

        public string Server { get; set; }
        public string Port { get; set; }

        private RelayCommand saveSettingsCommand;
        public ICommand SaveSettingsCommand
        {
            get { return saveSettingsCommand ?? (saveSettingsCommand = new RelayCommand(async () => await ExecuteSaveSettingsCommand())); }
        }

        [Insights]
        public async Task ExecuteSaveSettingsCommand()
        {
            int port;
            int.TryParse(Port, out port);

            Settings.SevendaysServer = Server;
            Settings.SevendaysPort = port.ToString();
        }
    }
}
