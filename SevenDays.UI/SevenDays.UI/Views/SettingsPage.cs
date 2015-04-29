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
            var serverLabel = new Label
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
                TextColor = Color.White,
				Text = "Server",
				StyleId = "serverLabel"
            };
			var serverText = new Entry 
			{
				StyleId = "serverEntry"
			};
            serverText.SetBinding(Entry.TextProperty, "Server");

            var portLabel = new Label
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
                TextColor = Color.White,
				Text = "Port",
				StyleId = "portLabel"
            };
            var portText = new Entry
			{
				StyleId = "portEntry"
			};
            portText.SetBinding(Entry.TextProperty, "Port");

            var button = new Button
            {
				Text = "Save",
				StyleId = "saveButton"
            };

            button.Clicked += OnButtonClicked;

            var layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(10),
                Children = {
                    serverLabel, serverText,
                    portLabel, portText,
                    button }
            };

            Content = layout;
            BackgroundColor = Color.Black;
            Title = "Seven Days Server Settings";
			StyleId = "settingsPage";
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            ViewModel.ExecuteSaveSettingsCommand();
            bool canConnect = false;
            using (var loading = UserDialogs.Instance.Loading("Checking connectivity..."))
            {
                canConnect = await ViewModel.ExecuteCheckConnectivityCommand();
            }
            if (!canConnect)
                await UserDialogs.Instance.AlertAsync("Unable to connect to server. Please check host and port.");
            else
                await Navigation.PopAsync();
        }
    }
}
