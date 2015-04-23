using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.UI.Templates;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
            Icon = "menu.png";
            Title = "menu"; // The Title property must be set.
            BackgroundColor = Color.FromHex("333333");            

            var cell = new DataTemplate(typeof(MenuCell));
			cell.SetBinding (MenuCell.TextProperty, "Title");
			cell.SetBinding (MenuCell.ImageSourceProperty, "IconSource");

            var menuItems = new List<Model.MenuItem>
            {
                new Model.MenuItem { 
				    Title = "Players", 
				    IconSource = "people.png", 
				    TargetType = typeof(PlayerListPage)
			    },
                new Model.MenuItem { 
                    Title = "Map", 
                    IconSource = "map.png", 
                    TargetType = typeof(MapPage)
                },
                new Model.MenuItem { 
				    Title = "Settings", 
				    IconSource = "settings.png", 
				    TargetType = typeof(SettingsPage)
			    },
                new Model.MenuItem { 
                    Title = "About", 
                    IconSource = "info.png", 
                    TargetType = typeof(AboutPage)
                }
            };

            Menu = new ListView
            { 
                ItemsSource = menuItems,
			    VerticalOptions = LayoutOptions.FillAndExpand,
			    BackgroundColor = Color.Transparent,
			    SeparatorVisibility = SeparatorVisibility.None,
			    ItemTemplate = cell,
            };

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "MENU",
                }
            };

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { menuLabel, Menu }
            };

            Content = layout;
        }
    }
}
