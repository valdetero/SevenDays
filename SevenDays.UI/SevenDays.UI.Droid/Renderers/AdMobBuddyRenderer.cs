using AdMobBuddy.Forms.Plugin.Abstractions;
using AdMobBuddy.Forms.Plugin.Droid;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SevenDays.Core;
using SevenDays.Core.Helpers;

[assembly: ExportRenderer(typeof(AdMobBuddyControl), typeof(AdMobBuddyRenderer))]
namespace AdMobBuddy.Forms.Plugin.Droid
{
    /// <summary>
    /// AdMobBuddy control Renderer for Android
    /// </summary>
    public class AdMobBuddyRenderer : ViewRenderer
    {
		static Android.Content.Context _context;
		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public static void Init(Android.Content.Context context)
		{
			_context = context;
		}

        /// <summary>
        /// reload the view and hit up google admob
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            //convert the element to the control we want
            var adMobElement = Element as AdMobBuddyControl;

            if ((adMobElement != null) && (e.OldElement == null))
            {
                AdView ad = new AdView(this.Context);
                ad.AdSize = AdSize.Banner;
                ad.AdUnitId = adMobElement.AdUnitId;
                var request = new AdRequest.Builder();
#if DEBUG
				var identifier = Android.Provider.Settings.Secure.GetString(_context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
				var currentDevice = MD5.GetMd5String(identifier).ToUpper();
				if (!ApiConstants.GoogleAds.Devices.Contains(currentDevice))
					ApiConstants.GoogleAds.Devices.Add(currentDevice);

				foreach (var device in ApiConstants.GoogleAds.Devices)
					request.AddTestDevice(device);
				request.AddTestDevice(AdRequest.DeviceIdEmulator);
#endif
				ad.LoadAd(request.Build());
                this.SetNativeControl(ad);
            }
        }
    }
}