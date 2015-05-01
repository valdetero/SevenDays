using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AdMobBuddy.Forms.Plugin.Abstractions
{
    public class AdMobBuddyControl : View
    {
        /// <summary>
        /// The ID of the AdMob ad to display
        /// This is the string Id from your Google Play account
        /// </summary>
        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create<AdMobBuddyControl, string>(p => p.AdUnitId, "");

        /// <summary>
        /// The ID of the AdMob ad to display
        /// This is the string Id from your Google Play account
        /// </summary>
        public string AdUnitId
        {
            get { return (string)GetValue(AdUnitIdProperty); }
            set { SetValue(AdUnitIdProperty, value); }
        }
    }
}
