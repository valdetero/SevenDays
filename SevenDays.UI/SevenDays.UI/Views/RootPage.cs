using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    public class RootPage : MasterDetailPage
    {
        MenuPage menuPage;
        private readonly ISettings settings;
        public RootPage()
        {
            settings = Container.Resolve<ISettings>();
            menuPage = new MenuPage();

            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as Model.MenuItem);

            Master = menuPage;

            ContentPage page;

            //bool isNoServerSelected = string.IsNullOrEmpty(settings.SevendaysSelectedServer);
            //if (isNoServerSelected)
            //    page = new ServerListPage();
            //else
                page = new PlayerListPage();

            Detail = new NavigationPage(page) 
            {
                BarBackgroundColor = Color.Teal,
                BarTextColor = Color.White,
				StyleId = "navigationPage"
            };

			StyleId = "rootPage";
        }

        void NavigateTo(Model.MenuItem menu)
        {
            if (menu == null)
                return;

            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            Detail = new NavigationPage(displayPage)
            {
                BarBackgroundColor = Color.Teal,
                BarTextColor = Color.White,
            };

            menuPage.Menu.SelectedItem = null;
            IsPresented = false;
        }
    }
}
