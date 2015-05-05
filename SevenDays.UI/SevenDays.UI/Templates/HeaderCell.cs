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
            Height = 40;
            var groupKey = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center
            };
            groupKey.SetBinding(Label.TextProperty, new Binding("Key"));

            View = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 40,
                BackgroundColor = Color.FromRgb(52, 152, 218),
                Padding = 5,
                Orientation = StackOrientation.Horizontal,
                Children = { groupKey }
            };
        }
    }
}
