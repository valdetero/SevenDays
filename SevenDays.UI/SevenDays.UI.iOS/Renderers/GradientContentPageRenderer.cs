using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using SevenDays.UI.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SevenDays.UI.Views.GradientContentPage), typeof(SevenDays.UI.iOS.Renderers.GradientContentPageRenderer))]

namespace SevenDays.UI.iOS.Renderers
{
    public class GradientContentPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null) // perform initial setup
            {
                var page = e.NewElement as GradientContentPage;
                var gradientLayer = new CoreAnimation.CAGradientLayer();
                gradientLayer.Frame = View.Bounds;
                gradientLayer.Colors = new CoreGraphics.CGColor[] { page.StartColor.ToCGColor(), page.EndColor.ToCGColor() };
                View.Layer.InsertSublayer(gradientLayer, 0);
            }
        }
    }
}