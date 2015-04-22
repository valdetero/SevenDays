using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SevenDays.Core;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SevenDays.UI.Views.Banner), typeof(SevenDays.UI.Droid.Renderers.BannerRenderer))]

namespace SevenDays.UI.Droid.Renderers
{
    public class BannerRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                AdView ad = new AdView(this.Context);
                ad.AdSize = AdSize.Banner;
                ad.AdUnitId = ApiConstants.GoogleAds.DroidKey;
                var requestbuilder = new AdRequest.Builder();
                ad.LoadAd(requestbuilder.Build());
                this.SetNativeControl(ad);
            }
        }
    }
}