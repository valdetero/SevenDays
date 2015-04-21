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
        public struct Steam
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
            
        }

        public struct Seven
        {
            public const string PlayerLocation = "api/getplayerslocation?";
            public const string PlayerInventory = "api/getplayerinventory?";
            public const string InventoryImage = "static/itemimages/{0}.png";
        }

        public struct Insights
        {
            public readonly static string Key = "";
        }        
    }
}
