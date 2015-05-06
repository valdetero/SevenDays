using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using GoogleAdMobAds;
using SevenDays.Core;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(SevenDays.UI.Controls.Banner), typeof(SevenDays.UI.iOS.Renderers.BannerRenderer))]

namespace SevenDays.UI.iOS.Renderers
{
    public class BannerRenderer : ViewRenderer
    {
        GADBannerView adView;
        bool viewOnScreen = false;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            adView = new GADBannerView(size: GADAdSizeCons.Banner)//, origin: new PointF(0, 0))
            {
                AdUnitID = ApiConstants.GoogleAds.iOSKey,
                RootViewController = UIApplication.SharedApplication.Windows[0].RootViewController
            };

            adView.AdReceived += (sender, args) =>
            {
                if (!viewOnScreen) this.AddSubview(adView);
                viewOnScreen = true;
            };

            adView.LoadRequest(GADRequest.Request);
            base.SetNativeControl(adView);
        }
    }
}