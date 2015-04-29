using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.ViewModels;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    //public class AboutPageBase : ViewPage<AboutViewModel> { }

    public class AboutPage : ContentPage
    {
        public AboutPage ()
	    {
            Title = "About";

            string nugeturl = "https://www.nuget.org/packages/";
            var source = new HtmlWebViewSource 
            {
                Html = string.Format(@"
<div><h3>Credits</h3>
Icons made by 
<a href='http://www.flaticon.com/authors/freepik' title='Freepik'>Freepik</a>, 
<a href='http://www.flaticon.com/authors/google' title='Google'>Google</a> from 
<a href='http://www.flaticon.com' title='Flaticon'>www.flaticon.com</a>             is licensed by 
<a href='http://creativecommons.org/licenses/by/3.0/' title='Creative Commons BY 3.0'>CC BY 3.0</a>
</div>
<div><h3>Nugets</h3>
<a href='{0}Acr.UserDialogs' title='Acr.UserDialogs'>Acr.UserDialogs</a><br />
<a href='{0}AutoMapper' title='AutoMapper'>AutoMapper</a><br />
<a href='{0}Fody' title='Fody'>Fody</a><br />
<a href='{0}MethodDecoratorEx.Fody' title='MethodDecoratorEx.Fody'>MethodDecoratorEx.Fody</a><br />
<a href='{0}PropertyChanged.Fody' title='PropertyChanged.Fody'>PropertyChanged.Fody</a><br />
<a href='{0}Microsoft.Net.Http' title='Microsoft.Net.Http'>Microsoft.Net.Http</a><br />
<a href='{0}modernhttpclient' title='modernhttpclient'>modernhttpclient</a><br />
<a href='{0}Newtonsoft.Json' title='Newtonsoft.Json'>Newtonsoft.Json</a><br />
<a href='{0}PCLStorage' title='PCLStorage'>PCLStorage</a><br />
<a href='{0}Toasts.Forms.Plugin' title='Toasts.Forms.Plugin'>Toasts.Forms.Plugin</a><br />
<a href='{0}Xam.Plugin.Connectivity' title='Xam.Plugin.Connectivity'>Xam.Plugin.Connectivity</a><br />
<a href='{0}Xam.Plugin.DeviceInfo' title='Xam.Plugin.DeviceInfo'>Xam.Plugin.DeviceInfo</a><br />
<a href='{0}Xam.Plugins.Forms.ImageCircle' title='Xam.Plugins.Forms.ImageCircle'>Xam.Plugins.Forms.ImageCircle</a><br />
<a href='{0}Xam.Plugins.Settings' title='Xam.Plugins.Settings'>Xam.Plugins.Settings</a><br />
<a href='{0}Xamarin.Forms' title='Xamarin.Forms'>Xamarin.Forms</a><br />
<a href='{0}Xamarin.Insights' title='Xamarin.Insights'>Xamarin.Insights</a><br />
</div>", nugeturl)
			};

			StyleId = "aboutPage";

            Content = new WebView
            {
                Source = source,
				StyleId = "aboutWebView"
            };
	    }
    }
}
