using SevenDays.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SevenDays
{
	/*
    public static class Analytics
    {
        public static void Init()
        {
#if !TEST
            Xamarin.Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
            {
                if (isStartupCrash)
                {
                    Xamarin.Insights.PurgePendingCrashReports().Wait();
                }
            };
    #if DROID
            Xamarin.Insights.Initialize(ApiConstants.Insights.Key, (Android.App.Activity)Xamarin.Forms.Forms.Context);
    #else
            Xamarin.Insights.Initialize(ApiConstants.Insights.Key);
    #endif
            Xamarin.Insights.Identify(Plugin.DeviceInfo.CrossDeviceInfo.Current.Id, new Dictionary<string, string>());
    #if DEBUG
            Xamarin.Insights.ForceDataTransmission = true;
    #endif
            Xamarin.Insights.DisableCollection = false;
            Xamarin.Insights.DisableDataTransmission = false;
            Xamarin.Insights.DisableExceptionCatching = false;
#else
    #if DROID
            Xamarin.Insights.Initialize(Xamarin.Insights.DebugModeKey, (Android.App.Activity)Xamarin.Forms.Forms.Context);
    #else
            Xamarin.Insights.Initialize(Xamarin.Insights.DebugModeKey);
    #endif
            //Xamarin.Insights.DisableCollection = true;
            //Xamarin.Insights.DisableDataTransmission = true;
            //Xamarin.Insights.DisableExceptionCatching = true;
#endif
        }
    }
	*/
}
