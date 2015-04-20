using SevenDays.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SevenDays
{
    public static class Analytics
    {
        public static void Init()
        {
#if !TEST
    #if DROID
            Xamarin.Insights.Initialize(ApiConstants.Insights.Key, (Android.App.Activity)Xamarin.Forms.Forms.Context);
    #else
            Xamarin.Insights.Initialize(ApiConstants.Insights.Key);
    #endif
            Xamarin.Insights.Identify(DeviceInfo.Plugin.CrossDeviceInfo.Current.Id, new Dictionary<string, string>());
            Xamarin.Insights.ForceDataTransmission = true;
            Xamarin.Insights.DisableCollection = false;
            Xamarin.Insights.DisableDataTransmission = false;
            Xamarin.Insights.DisableExceptionCatching = false;
#endif
        }
    }
}
