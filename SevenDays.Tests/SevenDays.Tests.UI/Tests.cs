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
		static readonly Func<AppQuery, AppQuery> ServerText = x => x.Marked("serverEntry");
		static readonly Func<AppQuery, AppQuery> PortText = x => x.Marked("portEntry");
		static readonly Func<AppQuery, AppQuery> Delete = x => x.Class("UIButton").Marked("Delete");
		static readonly Func<AppQuery, AppQuery> Favorite = x => x.Class("UIButton").Marked("Favorite");

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

        //[Test]
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

		[Test]
		public void Add_and_Delete_Server()
		{
			app.WaitForNoElement(Toast);

			app.Tap("plus");

			//app.Tap("serverEntry");

			app.WaitThenEnterText(ServerText, "seth-7dtd.cloudapp.net");

			//app.EnterText("misko-7dtd.cloudapp.net");

			app.WaitThenEnterText(PortText, "8082");

			app.Tap("saveButton");

			//need to account for navigation?

			var server = app.Query("nameLabel");
			Assert.IsTrue(server.Any());

			app.Screenshot("Server added");

			app.DragCoordinates(270, 100, 20, 100);

			app.WaitForElement(Delete);

			app.Tap(Delete);

			var server2 = app.Query("nameLabel");
			Assert.IsFalse(server2.Any());
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

