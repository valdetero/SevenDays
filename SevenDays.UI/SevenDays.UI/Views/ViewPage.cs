using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    public class ViewPage<T> : ContentPage where T : IViewModel, new()
    {
        T _viewModel;

        public T ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value;
            }
        }

        public ViewPage()
        {
            _viewModel = new T();
            BindingContext = _viewModel;
        }
    }
}
