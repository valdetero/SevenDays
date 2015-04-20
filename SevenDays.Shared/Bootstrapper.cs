using System;
using System.Collections.Generic;
using System.Text;

namespace SevenDays
{
    public static class Bootstrapper
    {
        public static void Setup()
        {
            IoC.Init();
            SevenDays.Model.Mapper.AutoMapperConfig.Register();

#if !TEST

            Analytics.Init();

    #if DROID
            Acr.UserDialogs.UserDialogs.Init(() => (Android.App.Activity)Xamarin.Forms.Forms.Context);
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
    #elif __IOS__
            Acr.UserDialogs.UserDialogs.Init();
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
    #elif WINDOWS_PHONE
            Acr.UserDialogs.UserDialogs.Init();
            ImageCircle.Forms.Plugin.WindowsPhone.ImageCircleRenderer.Init();
    #endif

#endif

        }
    }
}
