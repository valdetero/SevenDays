using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AdMobBuddy.Forms.Plugin.Abstractions;
using SevenDays.Core;
using SevenDays.Core.ViewModels;
using SevenDays.UI.Controls;
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
            listView = new InfiniteListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(PlayerCell)),
                SeparatorColor = Color.FromHex("#ddd"),
                IsPullToRefreshEnabled = true,
                Header = new Image { Source = PlatformImage.Resolver("header.jpg") },
				BackgroundColor = Color.Black,
                LoadMoreCommand = ViewModel.LoadMoreCommand,
                ItemsSource = ViewModel.Players,
				StyleId = "playerListView"
            };

            listView.ItemTapped += OnItemSelected;
            listView.Refreshing += OnListViewRefreshing;
            
			StyleId = "playerListPage";
            
            var layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                Children = { listView }
            };

            if (SevenDays.Core.Helpers.Settings.ShowAds)
                layout.Children.Add(new AdMobBuddyControl { AdUnitId = App.AdMobId, StyleId = "admobControl" });

            Content = layout;
        }

        protected async void OnListViewRefreshing(object sender, EventArgs e)
        {
            await loadGrid();
            listView.EndRefresh();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await loadGrid();
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
            ViewModel.IsBusy = false;

            using (var loading = UserDialogs.Instance.Loading("Loading players..."))
            {
                await ViewModel.ExecuteLoadMoreCommand();
            }

            listView.SelectedItem = null;

            ViewModel.IsBusyChanged = (busy) =>
            {
                IsBusy = busy;
            };

            if (!ViewModel.Players.Any())
            {
                var notificator = DependencyService.Get<IToastNotificator>();
                bool tapped = await notificator.Notify(ToastNotificationType.Info, "Players", "No players have connected", TimeSpan.FromSeconds(2));
            }
        }

        private async Task<bool> serverIsReachable()
        {
            IsBusy = true;

            var isReachable = await ViewModel.ExecuteIsServerReachableCommand();

            if(!isReachable)
            {
                bool tapped = await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Error, "Server Error", "Server is unavailable", TimeSpan.FromSeconds(2));
            }

            IsBusy = false;

            return isReachable;
        }
    }
}
