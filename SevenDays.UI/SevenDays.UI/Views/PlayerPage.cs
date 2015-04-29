using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SevenDays.Core.ViewModels;
using SevenDays.UI.Templates;
using Toasts.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace SevenDays.UI.Views
{
    public class PlayerPageBase : ViewPage<PlayerViewModel> { }

    public class PlayerPage : PlayerPageBase
    {
        private ListView listView;
        public PlayerPage()
        {
            listView = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(InventoryCell)),
                SeparatorColor = Color.FromHex("#ddd"),
                IsPullToRefreshEnabled = true,
                BackgroundColor = Color.Black,
                IsGroupingEnabled = true,
				GroupDisplayBinding = new Binding("Key"),
				StyleId = "inventoryListView"
            };
            if (Device.OS != TargetPlatform.WinPhone)
                listView.GroupHeaderTemplate = new DataTemplate(typeof(HeaderCell));

            listView.Refreshing += OnListViewRefreshing;
            Content = listView;
            Title = ViewModel.Name;
			StyleId = "playerPage";
        }
        public PlayerPage(PlayerViewModel viewModel) : this()
        {
            ViewModel = viewModel;
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

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var player = (PlayerViewModel)BindingContext;

            ViewModel = player;
        }

        private async Task loadGrid()
        {
            IsBusy = true;
            using (var loading = UserDialogs.Instance.Loading("Loading inventory..."))
            {
                await ViewModel.ExecuteLoadInventoryCommand();
                listView.ItemsSource = ViewModel.Inventory;
            }
            IsBusy = false;

            if (!ViewModel.Inventory.Any())
            {
                var notificator = DependencyService.Get<IToastNotificator>();
                bool tapped = await notificator.Notify(ToastNotificationType.Warning, "Inventory", "There are no items in the inventory", TimeSpan.FromSeconds(2));
            }
        }
    }
}
