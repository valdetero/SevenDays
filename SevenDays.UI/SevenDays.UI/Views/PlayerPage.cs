using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using SevenDays.Core.ViewModels;
using SevenDays.UI.Templates;
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
                GroupDisplayBinding = new Binding("Key")
            };
            if (Device.OS != TargetPlatform.WinPhone)
                listView.GroupHeaderTemplate = new DataTemplate(typeof(HeaderCell));

            listView.Refreshing += OnListViewRefreshing;
            Content = listView;
            Title = ViewModel.Name;
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
            using (var loading = UserDialogs.Instance.Loading("Loading inventory..."))
            {
                await ViewModel.ExecuteLoadInventoryCommand();
                listView.ItemsSource = ViewModel.Inventory;
            }
        }
    }
}
