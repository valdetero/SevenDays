﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SevenDays.Core;
using SevenDays.UI.Views;
using Xamarin.Forms;

namespace SevenDays.UI
{
    public class App : Application
    {        
        public App()
        {
            MainPage = new RootPage();
        }

        public static Xamarin.Forms.Color BrandColor { get { return Color.Blue; } }

        public static string AdMobId { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
