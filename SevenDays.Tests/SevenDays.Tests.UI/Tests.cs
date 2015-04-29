using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace SevenDays.Tests.UI
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	public class Tests
	{
		IApp app;
		Platform platform;

		static readonly Func<AppQuery, AppQuery> ProfileImages = x => x.Marked("profileImage");
		static readonly Func<AppQuery, AppQuery> LoadingInventoryDialog = x => x.Text("Loading inventory...");
		static readonly Func<AppQuery, AppQuery> LoadingPlayersDialog = x => x.Text("Loading players...");
		static readonly Func<AppQuery, AppQuery> Toast = x => x.Class("Toasts_Forms_Plugin_iOS_MessageView");
		static readonly Func<AppQuery, AppQuery> PullToRefresh = x => x.Class("UIRefreshControl");

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

        [Test]
        public void Open_Repl()
        {
            app.Repl();
        }

		[Test]
		public void Ensure_Navigation_To_Inventory_And_Back()
		{
			app.WaitForNoElement(LoadingPlayersDialog);

			app.Screenshot("Players loaded");

			var result = app.Query(ProfileImages).ToList();

			Assert.IsTrue(result.Any());

			app.Tap(ProfileImages);

			app.WaitForNoElement(LoadingInventoryDialog, "Timed out waiting for dialog", TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

			app.Screenshot("Inventory loaded");

			app.WaitForNoElement(Toast, "Timed out waiting for toast", TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

			app.Back();

			app.WaitForNoElement(LoadingPlayersDialog, "Timed out waiting for dialog", TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

			result = app.Query(ProfileImages).ToList();

			Assert.IsTrue(result.Any());
		}

		//Test fails in the simulator
		//http://forums.xamarin.com/discussion/29015/scrolling-the-simulator#latest
		[Test]
		public void Ensure_Pull_To_Refresh()
		{
			app.WaitForNoElement(LoadingPlayersDialog, "Timed out waiting for dialog", TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

			var result = app.Query(ProfileImages).ToList();

			Assert.IsTrue(result.Any());

			app.Screenshot("App load");

			app.DragCoordinates(200, 100, 200, 400);

			app.WaitForElement(LoadingPlayersDialog);

			var refresh = app.Query(LoadingPlayersDialog);

			Assert.IsTrue(refresh.Any(), "Unable to find loading fialog");

			app.WaitForNoElement(LoadingPlayersDialog, "Timed out waiting for dialog", TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

			app.Screenshot("Players loaded");

			result = app.Query(ProfileImages).ToList();

			Assert.IsTrue(result.Any());
		}
	}
}

