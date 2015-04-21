using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SevenDays.Core;
using SevenDays.Core.ViewModels;
using SevenDays.UI.Templates;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    public class PlayerListPageBase : ViewPage<PlayerListViewModel> { }

    public class PlayerListPage : PlayerListPageBase
    {
        private ListView listView;

        public PlayerListPage()
        {
            listView = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(PlayerCell)),
                SeparatorColor = Color.FromHex("#ddd"),
                IsPullToRefreshEnabled = true,
                Header = new Image { Source = ApiConstants.Steam.HeaderUrl },
                BackgroundColor = Color.Black,
            };

            listView.ItemTapped += OnItemSelected;
            listView.Refreshing += OnListViewRefreshing;
            Content = listView;

            this.ToolbarItems.Add(new ToolbarItem { Text = "Settings", Icon = "settings.png", Command = new Command(() => Navigation.PushModalAsync(new SettingsPage())) });
        }

        protected async void OnListViewRefreshing(object sender, EventArgs e)
        {
            await loadGrid();
            listView.EndRefresh();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.Players == null || ViewModel.Players.Count == 0)
            {
                await loadGrid();
            }
            listView.SelectedItem = null;
        }

        protected async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as PlayerViewModel;

            var selected = new PlayerPage
            {
                BindingContext = item
            };

            await Navigation.PushAsync(selected);
        }

        private async Task loadGrid()
        {
            using (var loading = UserDialogs.Instance.Loading("Loading players..."))
            {
                await ViewModel.ExecuteGetPlayerSummariesCommand();
                listView.ItemsSource = ViewModel.Players;
            }
        }
    }
}
