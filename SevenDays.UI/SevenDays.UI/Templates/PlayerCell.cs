using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCircle.Forms.Plugin.Abstractions;
using SevenDays.Core.ViewModels;
using SevenDays.Model;
using Xamarin.Forms;

namespace SevenDays.UI.Templates
{
    public class PlayerCell : ViewCell
    {
        public PlayerCell()
        {
            var profileImage = new CircleImage
            {
                BorderColor = Color.Teal,
                BorderThickness = 2,
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
				StyleId = "profileImage"
            };
            profileImage.SetBinding<PlayerViewModel>(Image.SourceProperty, x => x.Avatar);

            var nameLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
                TextColor = Color.White,
				StyleId = "nameLabel"
            };
            nameLabel.SetBinding<PlayerViewModel>(Label.TextProperty, x => x.Name);

            var steamLabel = new Label()
            {
                FontAttributes = FontAttributes.Bold,
				FontSize = 12,
				StyleId = "steamLabel"
            };
            steamLabel.SetBinding<PlayerViewModel>(Label.TextColorProperty, x => x.SteamColor);
            steamLabel.SetBinding<PlayerViewModel>(Label.TextProperty, x => x.SteamAvailability);

            var onlineImage = new Image()
            {
                Source = "online.png",
                HeightRequest = 8,
                WidthRequest = 8,
				StyleId = "onlineImage"
            };
            onlineImage.SetBinding<PlayerViewModel>(Image.IsVisibleProperty, x => x.ShouldShowAsOnline);
            var onLineLabel = new Label()
            {
                Text = "Online",
                TextColor = Color.Green,
                FontSize = 12,
				IsVisible = false,
				StyleId = "onlineLabel"
            };
            onLineLabel.SetBinding<PlayerViewModel>(Label.IsVisibleProperty, x => x.ShouldShowAsOnline);

            // Offline image and label
            var offlineImage = new Image()
            {
                Source = "offline.png",
                HeightRequest = 8,
				WidthRequest = 8,
				StyleId = "offlineImage"
            };
            offlineImage.SetBinding<PlayerViewModel>(Image.IsVisibleProperty, x => x.ShouldShowAsOffline);
            var offLineLabel = new Label()
            {
                Text = "Offline",
                TextColor = Color.FromHex("#ddd"),
                FontSize = 12,
				IsVisible = false,
				StyleId = "offlineLabel"
            };
            offLineLabel.SetBinding<PlayerViewModel>(Label.IsVisibleProperty, x => x.ShouldShowAsOffline);

            var statusLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { onlineImage, onLineLabel, offlineImage, offLineLabel, steamLabel }
            };

            var timeLabel = new Label
            {
                TextColor = Color.FromHex("#ddd"),
                FontAttributes = FontAttributes.Italic,
				FontSize = 10,
				StyleId = "timeLabel"
            };
            timeLabel.SetBinding<PlayerViewModel>(Label.TextProperty, x => x.LastLogOff);

            var detailsLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 0, 0),
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { nameLabel, statusLayout, timeLabel }
            };

            var tapImage = new Image()
            {
                Source = "tap.png",
                HorizontalOptions = LayoutOptions.End,
				HeightRequest = 12,
				StyleId = "tapImage"
            };

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { profileImage, detailsLayout, tapImage }
            };

			StyleId = "playerCell";
            this.View = cellLayout;
        }
    }
}