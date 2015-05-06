using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleAds;
using SevenDays.Core;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(SevenDays.UI.Controls.Banner), typeof(SevenDays.UI.WinPhone.Renderers.BannerRenderer))]

namespace SevenDays.UI.WinPhone.Renderers
{
    public class BannerRenderer : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                AdView bannerAd = new AdView
                {
                    Format = AdFormats.Banner,
                    AdUnitID = ApiConstants.GoogleAds.WinKey,
                };
                AdRequest adRequest = new AdRequest();
                bannerAd.LoadAd(adRequest);
                Children.Add(bannerAd);
            }
        }
    }
}
