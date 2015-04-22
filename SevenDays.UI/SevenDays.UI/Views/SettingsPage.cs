using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SevenDays.Core.ViewModels;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    public class SettingsPageBase : ViewPage<SettingsViewModel> { }

    public class SettingsPage : SettingsPageBase
    {
        public SettingsPage()
        {            
            var serverLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
                TextColor = Color.White,
                Text = "Server"
            };
            var serverText = new Entry
            {

            };
            serverText.SetBinding(Entry.TextProperty, "Server");

            var serverLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { serverLabel, serverText,  }
            };

            var portLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
                TextColor = Color.White,
                Text = "Port"
            };
            var portText = new Entry
            {

            };
            portText.SetBinding(Entry.TextProperty, "Port");

            var portLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { portLabel, portText, }
            };

            var button = new Button
            {
                Text = "Save",

            };

            button.Clicked += OnButtonClicked;

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { serverLayout, portLayout, button }
            };

            Content = layout;
            BackgroundColor = Color.Black;
            Title = "Seven Days Server Settings";
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            await ViewModel.ExecuteSaveSettingsCommand();
            bool canConnect = false;
            using (var loading = UserDialogs.Instance.Loading("Checking connectivity..."))
            {
                canConnect = await ViewModel.ExecuteCheckConnectivityCommand();
            }
            if (!canConnect)
                UserDialogs.Instance.AlertAsync("Unable to connect to server. Please check host and port.");
            else
                Navigation.PopAsync();
        }
    }
}
