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

            ContextActions.Add(favoriteAction);
            ContextActions.Add(deleteAction);

            var image = new Image
            {
                Source = PlatformImage.Resolver("favorite.png"),
                HeightRequest = 40,
                WidthRequest = 40,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            image.SetBinding<ServerViewModel>(Image.IsVisibleProperty, x => x.IsFavorite);

            var notimage = new Image
            {
                HeightRequest = 40,
                WidthRequest = 40,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            notimage.SetBinding<ServerViewModel>(Image.IsVisibleProperty, x => x.IsNotFavorite);

            var nameLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
                TextColor = Color.White,
                StyleId = "nameLabel"
            };
            nameLabel.SetBinding<ServerViewModel>(Label.TextProperty, x => x.Host);

            var detailsLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 0, 0),
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Children = { nameLabel }
            };

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 10, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { image, notimage, detailsLayout }
            };

            StyleId = "serverCell";
            this.View = cellLayout;
        }
    }
}
