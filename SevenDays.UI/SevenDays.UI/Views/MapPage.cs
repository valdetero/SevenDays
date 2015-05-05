using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.ViewModels;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    public class MapPageBase : ViewPage<MapViewModel> { }

    public class MapPage : MapPageBase
    {
        private WebView webView;
        public MapPage ()
	    {
            Title = "Server Map";
			StyleId = "mapPage";
            webView = new WebView
            {
				StyleId = "mapWebView"
            };
            Content = webView;
	    }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            webView.Source = await ViewModel.ExecuteGetMapUrlCommand();
        }
    }
}
