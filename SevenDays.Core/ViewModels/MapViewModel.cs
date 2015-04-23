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
    public class MapViewModel : IViewModel
    {
        private ISevendayService sevendayService;
        public MapViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
        }

        private RelayCommand getGetMapUrlCommand;
        public ICommand GetMapUrlCommand
        {
            get { return getGetMapUrlCommand ?? (getGetMapUrlCommand = new RelayCommand(() => ExecuteGetMapUrlCommand())); }
        }

        [Insights]
        public string ExecuteGetMapUrlCommand()
        {
            return sevendayService.GetMapUrl();
        }
    }
}
