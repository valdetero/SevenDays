using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SevenDays.Core;
using SevenDays.Core.ViewModels;
using SevenDays.UI.Templates;
using Toasts.Forms.Plugin.Abstractions;
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

            var selected = new PlayerPage(item)
            {
                BindingContext = item
            };

            await Navigation.PushAsync(selected);
        }

        private async Task loadGrid()
        {
            IsBusy = true;
            using (var loading = UserDialogs.Instance.Loading("Loading players..."))
            {
                await ViewModel.ExecuteGetPlayerSummariesCommand();
                listView.ItemsSource = ViewModel.Players;
            }
            IsBusy = false;

            if (!ViewModel.Players.Any())
            {
                var notificator = DependencyService.Get<IToastNotificator>();
                bool tapped = await notificator.Notify(ToastNotificationType.Warning, "Players", "No players have connected to the server", TimeSpan.FromSeconds(2));
            }
        }
    }
}
