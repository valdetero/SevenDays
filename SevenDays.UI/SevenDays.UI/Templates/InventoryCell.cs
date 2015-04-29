using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace SevenDays.UI.Templates
{
    public class InventoryCell : ViewCell
    {
        public InventoryCell()
        {
            //116px x 80px
            var invImage = new Image
            {
                //BorderColor = Color.Teal,
                //BorderThickness = 2,
                HeightRequest = 60,
                WidthRequest = 87,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				StyleId = "inventoryImage"
            };
            invImage.SetBinding(Image.SourceProperty, "Image");

            var nameLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
				TextColor = Color.White,
				StyleId = "nameLabel"
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");

            var countLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = 12,
				TextColor = Color.White,
				StyleId = "countLabel"
            };
            countLabel.SetBinding(Label.TextProperty, "Count");

            var statusLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { countLabel }
            };

            var detailsLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 0, 0),
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { nameLabel, statusLayout }
            };

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { invImage, detailsLayout }
            };

			StyleId = "inventoryCell";
            this.View = cellLayout;
        }
    }
}
