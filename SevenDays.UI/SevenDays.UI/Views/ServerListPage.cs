using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.ViewModels;
using SevenDays.UI.Templates;
using Toasts.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    public class ServerListPageBase : ViewPage<ServerListViewModel> { }

    public class ServerListPage : ServerListPageBase
    {
        private ListView listView;

        public ServerListPage()
        {
            listView = new ListView
            {
                //HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(ServerCell)),
                SeparatorColor = Color.FromHex("#ddd"),
                BackgroundColor = Color.Black,
                StyleId = "serverListView"
            };

            listView.SetBinding<ServerListViewModel>(ListView.RefreshCommandProperty, x => x.GetServersCommand);
            listView.ItemTapped += OnItemSelected;
            listView.Refreshing += OnListViewRefreshing;

            StyleId = "serverListPage";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = { listView }
            };

            //TODO: Change "+" to image
            ToolbarItems.Add(new ToolbarItem("+", "", async () => await Navigation.PushAsync(new ServerPage())));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await loadGrid();

            if (!ViewModel.Servers.Any())
            {
                await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Info, "No Servers", "Please add a server", TimeSpan.FromSeconds(2));
            }
        }

        private async void OnListViewRefreshing(object sender, EventArgs e)
        {
            await loadGrid();
            listView.EndRefresh();
        }

        private async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as ServerViewModel;

            var selected = new ServerPage(item)
            {
                BindingContext = item
            };

            await Navigation.PushAsync(selected);
        }

        private async Task loadGrid()
        {
            //if (ViewModel.Servers == null || ViewModel.Servers.Count == 0)
            //{
            await ViewModel.ExecuteGetServersCommand();
            listView.ItemsSource = ViewModel.Servers;
            //}
            listView.SelectedItem = null;
        }
    }
}
