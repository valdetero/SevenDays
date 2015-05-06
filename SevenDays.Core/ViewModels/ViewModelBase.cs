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
                if (IsBusyChanged != null)
                    IsBusyChanged(isBusy);
            }
        }
        
        public bool CanLoadMore { get; set; }
    }
}
