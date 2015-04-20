using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Foundation;
using SevenDays.UI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PageRenderer), typeof(TitleNavigationRenderer))]

namespace SevenDays.UI.iOS.Renderers
{
    public class TitleNavigationRenderer : PageRenderer
    {
        public override void LoadView()
        {
            System.Diagnostics.Debug.WriteLine("In Loadview");
            base.LoadView();

            UIImageView titleView = new UIImageView(new RectangleF(0, 0, 150, 30));
            titleView.Image = new UIImage("banner.png");

            NavigationItem.TitleView = titleView;
        }

        public override void ViewDidLoad()
        {
            System.Diagnostics.Debug.WriteLine("In view did load");
            base.ViewDidLoad();

            UIImageView titleView = new UIImageView(new RectangleF(0, 0, 150, 30));
            titleView.Image = new UIImage("banner.png");

            NavigationItem.TitleView = titleView;
        }
    }
}