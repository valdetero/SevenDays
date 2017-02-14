using AdMobBuddy.Forms.Plugin.Abstractions;
using GoogleAdMobAds;
using UIKit;
using Xamarin.Forms;
using AdMobBuddy.Forms.Plugin.iOS;
using Xamarin.Forms.Platform.iOS;
using SevenDays.Core;
using SevenDays.Core.Helpers;

[assembly: ExportRenderer(typeof(AdMobBuddyControl), typeof(AdMobBuddyRenderer))]
namespace AdMobBuddy.Forms.Plugin.iOS
{
	/// <summary>
	/// AdMobBuddy Renderer for iOS
	/// </summary>
	public class AdMobBuddyRenderer : ViewRenderer
    {
        GADBannerView adView;
        bool viewOnScreen = false;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        /// <summary>
        /// reload the view and hit up google admob
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            //convert the element to the control we want
            var adMobElement = Element as AdMobBuddyControl;

            if (null != adMobElement)
            {
                adView = new GADBannerView(size: GADAdSizeCons.Banner)
                {
                    AdUnitID = adMobElement.AdUnitId,
                    RootViewController = UIApplication.SharedApplication.Windows[0].RootViewController
                };

                adView.AdReceived += (sender, args) =>
                {
                    if (!viewOnScreen) this.AddSubview(adView);
                    viewOnScreen = true;
                };

				var request = GADRequest.Request;
#if DEBUG
				var identifier = UIDevice.CurrentDevice.IdentifierForVendor.ToString();
				var currentDevice = MD5.GetMd5String(identifier);
				if(!ApiConstants.GoogleAds.Devices.Contains(currentDevice))
					ApiConstants.GoogleAds.Devices.Add(currentDevice);

				request.TestDevices = ApiConstants.GoogleAds.Devices?.ToArray();
#endif
				adView.LoadRequest(request);
                base.SetNativeControl(adView);
            }
        }
    }
}
