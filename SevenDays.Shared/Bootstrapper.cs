﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SevenDays
{
    public static class Bootstrapper
	{
#if DROID
		public static void Setup(Android.Content.Context context)
#else
		public static void Setup()
#endif
        {
            IoC.Init();
            SevenDays.Model.Mapper.AutoMapperConfig.Register();
            //Analytics.Init();

			SQLitePCL.Batteries_V2.Init();
            Akavache.BlobCache.ApplicationName = "SevenDays";
            Akavache.BlobCache.EnsureInitialized();
#if !TEST

#if DROID
            Acr.UserDialogs.UserDialogs.Init(() => (Android.App.Activity)Xamarin.Forms.Forms.Context);
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            Toasts.Forms.Plugin.Droid.ToastNotificatorImplementation.Init();
            AdMobBuddy.Forms.Plugin.Droid.AdMobBuddyRenderer.Init(context);
            SevenDays.UI.App.AdMobId = SevenDays.Core.ApiConstants.GoogleAds.DroidKey;
			Microsoft.Azure.Mobile.MobileCenter.Start(Core.ApiConstants.MobileCenter.Droid, typeof(Microsoft.Azure.Mobile.Analytics.Analytics), typeof(Microsoft.Azure.Mobile.Crashes.Crashes));
#elif __IOS__
			//Acr.UserDialogs.UserDialogs.Init();
			ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            Toasts.Forms.Plugin.iOS.ToastNotificatorImplementation.Init();
            AdMobBuddy.Forms.Plugin.iOS.AdMobBuddyRenderer.Init();
            SevenDays.UI.App.AdMobId = SevenDays.Core.ApiConstants.GoogleAds.iOSKey;
			Microsoft.Azure.Mobile.MobileCenter.Start(Core.ApiConstants.MobileCenter.iOS, typeof(Microsoft.Azure.Mobile.Analytics.Analytics), typeof(Microsoft.Azure.Mobile.Crashes.Crashes));

#elif WINDOWS_PHONE
            //Acr.UserDialogs.UserDialogs.Init();
            ImageCircle.Forms.Plugin.WindowsPhone.ImageCircleRenderer.Init();
            Toasts.Forms.Plugin.WindowsPhone.ToastNotificatorImplementation.Init();
            AdmobBuddy.Forms.Plugin.WindowsPhone.AdMobBuddyRenderer.Init();
            SevenDays.UI.App.AdMobId = SevenDays.Core.ApiConstants.GoogleAds.WinKey;
#endif

#endif

		}
    }
}
