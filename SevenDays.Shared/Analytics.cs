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

    #if DROID
            Xamarin.Insights.Initialize(ApiConstants.Insights.Key, (Android.App.Activity)Xamarin.Forms.Forms.Context);
    #else
            Xamarin.Insights.Initialize(ApiConstants.Insights.Key);
    #endif
            Xamarin.Insights.Identify(DeviceInfo.Plugin.CrossDeviceInfo.Current.Id, new Dictionary<string, string>());
#if !TEST
            Xamarin.Insights.ForceDataTransmission = true;
            Xamarin.Insights.DisableCollection = false;
            Xamarin.Insights.DisableDataTransmission = false;
            Xamarin.Insights.DisableExceptionCatching = false;
#else
            Xamarin.Insights.DisableCollection = true;
            Xamarin.Insights.DisableDataTransmission = true;
            Xamarin.Insights.DisableExceptionCatching = true;
#endif
        }
    }
}
