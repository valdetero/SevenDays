// Helpers/Settings.cs
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace SevenDays.Core.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SevendaysServerKey = "sevendays_server";
        //private static readonly string SevendaysServerDefault = "localhost";
        private static readonly string SevendaysServerDefault = "home.wtfnext.com";

        private const string SevendaysPortKey = "sevendays_port";
        private static readonly string SevendaysPortDefault = "26903";

        private const string SevendaysSelectedServerKey = "sevendays_selected_server";

        private const string UseAds = "showAds";

        #endregion


        public static string SevendaysServer
        {
            get { return AppSettings.GetValueOrDefault(SevendaysServerKey, SevendaysServerDefault); }
            set { AppSettings.AddOrUpdateValue(SevendaysServerKey, value); }
        }

        public static string SevendaysPort
        {
            get { return AppSettings.GetValueOrDefault(SevendaysPortKey, SevendaysPortDefault); }
            set { AppSettings.AddOrUpdateValue(SevendaysPortKey, value); }
        }

        public static string SevendaysSelectedServer
        {
            get { return AppSettings.GetValueOrDefault(SevendaysSelectedServerKey, ""); }
            set { AppSettings.AddOrUpdateValue(SevendaysSelectedServerKey, value); }
        }

        public static bool ShowAds
        {
            get { return AppSettings.GetValueOrDefault(UseAds, false); }
            set { AppSettings.AddOrUpdateValue(UseAds, value); }
        }
    }
}