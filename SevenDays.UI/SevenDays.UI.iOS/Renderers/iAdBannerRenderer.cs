using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using iAd;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(SevenDays.UI.Views.Banner), typeof(SevenDays.UI.iOS.Renderers.iAdBannerRenderer))]

namespace SevenDays.UI.iOS.Renderers
{
    public class iAdBannerRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            var adBannerView = new ADBannerView(new System.Drawing.Rectangle(0, 386, 320, 50));
            base.SetNativeControl(adBannerView);
        }
    }
}