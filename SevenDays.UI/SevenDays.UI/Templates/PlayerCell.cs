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
            };
            profileImage.SetBinding(Image.SourceProperty, "Avatar");

            var nameLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
                TextColor = Color.White
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");

            var steamLabel = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
            };
            steamLabel.SetBinding(Label.TextColorProperty, "SteamColor");
            steamLabel.SetBinding(Label.TextProperty, "SteamAvailability");

            var onlineImage = new Image()
            {
                Source = "online.png",
                HeightRequest = 8,
                WidthRequest = 8
            };
            onlineImage.SetBinding(Image.IsVisibleProperty, "ShouldShowAsOnline");
            var onLineLabel = new Label()
            {
                Text = "Online",
                TextColor = Color.Green,
                FontSize = 12,
                IsVisible = false,
            };
            onLineLabel.SetBinding(Label.IsVisibleProperty, "ShouldShowAsOnline");

            // Offline image and label
            var offlineImage = new Image()
            {
                Source = "offline.png",
                HeightRequest = 8,
                WidthRequest = 8
            };
            offlineImage.SetBinding(Image.IsVisibleProperty, "ShouldShowAsOffline");
            var offLineLabel = new Label()
            {
                Text = "Offline",
                TextColor = Color.FromHex("#ddd"),
                FontSize = 12,
                IsVisible = false,
            };
            offLineLabel.SetBinding(Label.IsVisibleProperty, "ShouldShowAsOffline");

            var statusLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { onlineImage, onLineLabel, offlineImage, offLineLabel, steamLabel }
            };

            var detailsLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 0, 0),
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { nameLabel, statusLayout }
            };

            var tapImage = new Image()
            {
                Source = "tap.png",
                HorizontalOptions = LayoutOptions.End,
                HeightRequest = 12,
            };

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { profileImage, detailsLayout, tapImage }
            };

            this.View = cellLayout;
        }
    }
}