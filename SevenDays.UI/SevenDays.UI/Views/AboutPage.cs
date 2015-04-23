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
            Content = new Label
            {
                Text = @"<div>Icons made by <a href='http://www.flaticon.com/authors/freepik' title='Freepik'>Freepik</a>, <a href='http://www.flaticon.com/authors/google' title='Google'>Google</a> from <a href='http://www.flaticon.com' title='Flaticon'>www.flaticon.com</a>             is licensed by <a href='http://creativecommons.org/licenses/by/3.0/' title='Creative Commons BY 3.0'>CC BY 3.0</a></div>"
            };
	    }
    }
}
