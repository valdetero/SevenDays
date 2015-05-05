using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core;
using SevenDays.Core.ViewModels;
using Xamarin.Forms;

namespace SevenDays.UI.Templates
{
    public class HeaderCell : ViewCell
    {
        public HeaderCell()
        {
            this.Height = 25;
            var title = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = Font.SystemFontOfSize(NamedSize.Small).FontSize,
                TextColor = Color.White,
				VerticalOptions = LayoutOptions.Center,
				StyleId = "titleLabel"
            };

            title.SetBinding<Grouping<string, InventoryViewModel>>(Label.TextProperty, x => x.Key);

			StyleId = "headerCell";

            View = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 25,
                BackgroundColor = Color.FromRgb(52, 152, 218),
                Padding = 5,
                Orientation = StackOrientation.Horizontal,
                Children = { title }
            };
        }
    }
}
