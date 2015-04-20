using Foundation;
using UIKit;
using MonoTouch.NUnit.UI;

namespace SevenDays.Tests.iOS
{

    [Register("AppDelegateNUnit")]
    public partial class AppDelegateNUnit : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        TouchRunner runner;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Bootstrapper.Setup();

            window = new UIWindow(UIScreen.MainScreen.Bounds);
            runner = new TouchRunner(window);

            runner.Add(System.Reflection.Assembly.GetExecutingAssembly());

            window.RootViewController = new UINavigationController(runner.GetViewController());

            window.MakeKeyAndVisible();

            return true;
        }
    }
}
