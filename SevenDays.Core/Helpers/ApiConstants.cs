using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PCLStorage;

namespace SevenDays.Core
{
    public partial class ApiConstants
    {
        public partial struct Steam
        {
            public readonly static string Key = "";
            public const string AppId = "251570";
            public const string HeaderUrl = "http://cdn.akamai.steamstatic.com/steam/apps/251570/header.jpg";
            public const string IconUrl = "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/251570/c8f826b116770525b68e7b4e37ad83ca044ae760.jpg";
            public const string BaseUrl = "http://api.steampowered.com/";
            public const string GlobalStats = BaseUrl + "ISteamUserStats/GetGlobalStatsForGame/v0001/?";
            public const string PlayerSummary = BaseUrl + "ISteamUser/GetPlayerSummaries/v0002/?";
            public const string UserStats = BaseUrl + "ISteamUserStats/GetUserStatsForGame/v0002/?";
            public const string Achievements = BaseUrl + "ISteamUserStats/GetPlayerAchievements/v0001/?";
            public const string Schema = BaseUrl + "ISteamUserStats/GetSchemaForGame/v0002/?";
            public const string ApiEndpoint = BaseUrl + "ISteamWebAPIUtil/GetSupportedAPIList/v0001/?";
        }

        public partial struct Seven
        {
            public const string PlayerLocation = "api/getplayerslocation?";
            public const string PlayerInventory = "api/getplayerinventory?";
            public const string InventoryImage = "itemicons/{0}__{1}.png?";
            public const string Map = "static/index.html?";
			public readonly static string User = "";
			public readonly static string Token = "";
		}

        public partial struct Insights
        {
            public readonly static string Key = "";
        }

		public partial struct TestCloud
		{
			public readonly static string Key = "";
		}

        public partial struct GoogleAds
        {
            public readonly static string WinKey = "";
            public readonly static string DroidKey = "";
            public readonly static string iOSKey = "";
			public readonly static List<string> Devices;
		}
    }
}
