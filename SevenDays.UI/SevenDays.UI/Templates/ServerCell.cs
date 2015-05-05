using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.ViewModels;
using Xamarin.Forms;

namespace SevenDays.UI.Templates
{
    public class ServerCell : ViewCell
    {
        public ServerCell()
        {
            var connectAction = new MenuItem { Text = "Test" };
            connectAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            connectAction.SetBinding<ServerViewModel>(MenuItem.CommandProperty, x => x.CheckConnectivityCommand);

            var favoriteAction = new MenuItem { Text = "Favorite" };
            favoriteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            favoriteAction.SetBinding<ServerViewModel>(MenuItem.CommandProperty, x => x.SetDefaultCommand);

            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.SetBinding<ServerViewModel>(MenuItem.CommandProperty, x => x.DeleteCommand);
            deleteAction.Clicked += async (sender, e) =>
            {
                var mi = (MenuItem)sender;
                var vm = (ServerViewModel)mi.CommandParameter;
            };

            ContextActions.Add(connectAction);
            ContextActions.Add(favoriteAction);
            ContextActions.Add(deleteAction);

            var nameLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
                TextColor = Color.White,
                StyleId = "nameLabel"
            };
            nameLabel.SetBinding<ServerViewModel>(Label.TextProperty, x => x.Host);

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { nameLabel }
            };

            StyleId = "serverCell";
            this.View = cellLayout;
        }
    }
}
