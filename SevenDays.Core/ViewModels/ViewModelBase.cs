using SevenDays.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ViewModelBase
    {
        private bool isBusy;
        public Action<bool> IsBusyChanged { get; set; }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
				IsBusyChanged?.Invoke(isBusy);
			}
        }

        public bool CanLoadMore { get; set; }

		protected ILogger Logger => Ioc.Container.Resolve<ILogger>();
		protected ITrackTimer TrackTime(string identifier)
		{
			return Ioc.Container.Resolve<ITrackTimer>().Init(identifier);
		}
    }
}
