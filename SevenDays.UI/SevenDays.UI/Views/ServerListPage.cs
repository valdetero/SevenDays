﻿using System;
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
                ItemTemplate = new DataTemplate(typeof(ServerCell)),
                SeparatorColor = Color.FromHex("#ddd"),
                BackgroundColor = Color.Black,
                IsPullToRefreshEnabled = true,
                HasUnevenRows = true,
                StyleId = "serverListView"
            };

            listView.SetBinding<ServerListViewModel>(ListView.RefreshCommandProperty, x => x.GetServersCommand);
            listView.ItemTapped += OnItemSelected;
            listView.Refreshing += OnListViewRefreshing;

            StyleId = "serverListPage";
            Title = "Servers";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Fill,
                Children = { listView }
            };

            ToolbarItems.Add(new ToolbarItem("add", PlatformImage.Resolver("plus.png"), async () => await Navigation.PushAsync(new ServerPage())));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await loadGrid();

            if (!ViewModel.Servers.Any())
            {
                await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Info, "No Servers", "Please add a server", TimeSpan.FromSeconds(2));
            }

            MessagingCenter.Subscribe<ServerCell, ServerViewModel>(this, "Delete", async(sender, arg) =>
            {
                ViewModel.Servers.Remove(arg);

                if (arg.IsFavorite)
                {
                    await DependencyService.Get<IToastNotificator>().Notify(ToastNotificationType.Warning, "No Favorite", "Please select a favorite", TimeSpan.FromSeconds(3));
                }
            });
            MessagingCenter.Subscribe<ServerCell, ServerViewModel>(this, "Favorite", (sender, arg) =>
            {
                foreach (var server in ViewModel.Servers)
                {
                    server.IsFavorite = (server == arg);
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<ServerCell, ServerViewModel>(this, "Delete");
            MessagingCenter.Unsubscribe<ServerCell, ServerViewModel>(this, "Favorite");
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
            await ViewModel.ExecuteGetServersCommand();
            listView.ItemsSource = ViewModel.Servers;
            listView.SelectedItem = null;
        }
    }
}
