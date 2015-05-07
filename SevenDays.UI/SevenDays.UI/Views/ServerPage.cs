using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SevenDays.Core.ViewModels;
using SevenDays.UI.Controls;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    public class ServerPageBase : ViewPage<ServerViewModel> { }

    public class ServerPage : ServerPageBase
    {
        public ServerPage()
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
            serverText.SetBinding<ServerViewModel>(Entry.TextProperty, x => x.Host);

            var portLabel = new Label
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = Device.OnPlatform(18, 18, 24),
                TextColor = Color.White,
                Text = "Port",
                StyleId = "portLabel"
            };
            var portText = new DoneEntry
            {
                Keyboard = Keyboard.Numeric,
                StyleId = "portEntry",
            };
            portText.SetBinding<ServerViewModel>(Entry.TextProperty, x => x.Port);

            serverText.Completed += (sender, e) =>  portText.Focus();

            var save = new Button
            {
                Text = "Save",
                StyleId = "saveButton"
            };

            save.Clicked += OnSaveButtonClicked;

            var delete = new Button
            {
                Text = "Delete",
                StyleId = "deleteButton",
                TextColor = Color.Red                
            };

            delete.Clicked += OnDeleteButtonClicked;

            var layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(10),
                Children = {
                    serverLabel, serverText,
                    portLabel, portText,
                    save, delete }
            };

            Content = layout;
            BackgroundColor = Color.Black;
            Title = "Server Settings";
            StyleId = "settingsPage";
        }

        public ServerPage(ServerViewModel viewModel) : this()
        {
            ViewModel = viewModel;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var saved = await ViewModel.ExecuteSaveCommand();
            if(!saved)
            {
                await UserDialogs.Instance.AlertAsync("Unable to save. Please try again");
                return;
            }
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

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (await UserDialogs.Instance.ConfirmAsync("Delete?", "Are you sure that you want to delete this server?"))
            {
                await ViewModel.ExecuteDeleteCommand();
                await Navigation.PopAsync();
            }
        }
    }
}
