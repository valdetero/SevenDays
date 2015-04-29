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
        public MapPage ()
	    {
            Title = "Server Map";
			StyleId = "mapPage";
            Content = new WebView
            {
                Source = ViewModel.ExecuteGetMapUrlCommand(),
				StyleId = "mapWebView"
            };    
	    }
    }
}
