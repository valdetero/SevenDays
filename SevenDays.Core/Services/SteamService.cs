using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Model.Base;
using SevenDays.Model.Steam;
using Xamarin;

namespace SevenDays.Core.Services
{
    public class SteamService : ISteamService
    {
        private INetworkService networkService;
        public SteamService()
        {
            networkService = Ioc.Container.Resolve<INetworkService>();
        }

        private async Task<bool> canConnectToServer()
        {
            return await networkService.CanConnectToService(string.Format("http://{0}", Settings.SevendaysServer), Settings.SevendaysPort);
        }

        public async Task<Response<PlayerStats>> GetPlayerAchievements(long steamId)
        {            
            var response = new Response<PlayerStats>();

            if (!await canConnectToServer())
                return response;

            Insights.Track(string.Format("Getting player achievements for {0}", steamId));

            string url = string.Format("{0}key={1}&appId={2}&steamId={3}", ApiConstants.Steam.Achievements, ApiConstants.Steam.Key, ApiConstants.Steam.AppId, steamId);

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var result = await client.GetStringAsync(url);
                response.Result = JsonConvert.DeserializeObject<PlayerStats>(result);
            }

            return response;
        }

        public async Task<ListResponse<Player>> GetPlayerSummaries(params long[] steamIds)
        {
            var response = new ListResponse<Player>();

            if (!await canConnectToServer())
                return response;

            var sb = new StringBuilder();

            for (int i = 0; i < steamIds.Length; i++)
            {
                sb.Append(steamIds[i]);
                if (steamIds.Length > 1 && i + 1 != steamIds.Length)
                {
                    sb.Append(",");
                }
            }

            Insights.Track(string.Format("Getting player summaries for {0}", sb.ToString()));

            string url = string.Format("{0}key={1}&steamIds={2}", ApiConstants.Steam.PlayerSummary, ApiConstants.Steam.Key, sb.ToString());

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var result = await client.GetStringAsync(url);
                var data = JsonConvert.DeserializeObject<PlayerSummaryDto>(result);
                response.Result = data.Response.Players;
            }

            return response;
        }

        public async Task<Response<Game>> GetSchemaForGame(long steamId)
        {
            var response = new Response<Game>();

            if (!await canConnectToServer())
                return response;

            string url = string.Format("{0}key={1}&appId={2}", ApiConstants.Steam.Schema, ApiConstants.Steam.Key, ApiConstants.Steam.AppId);

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var result = await client.GetStringAsync(url);
                response.Result = JsonConvert.DeserializeObject<Game>(result);
            }

            return response;
        }

        public async Task<Response<PlayerStats>> GetUserStatsForGame(long steamId)
        {
            var response = new Response<PlayerStats>();

            if (!await canConnectToServer())
                return response;

            string url = string.Format("{0}key={1}&appId={2}&steamId={3}", ApiConstants.Steam.UserStats, ApiConstants.Steam.Key, ApiConstants.Steam.AppId, steamId);

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var result = await client.GetStringAsync(url);
                response.Result = JsonConvert.DeserializeObject<PlayerStats>(result);
            }

            return response;
        }
    }
}
