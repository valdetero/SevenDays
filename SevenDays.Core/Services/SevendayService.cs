using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using Connectivity.Plugin;
using ModernHttpClient;
using Newtonsoft.Json;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Model.Base;
using SevenDays.Model.Seven;
using Xamarin;

namespace SevenDays.Core.Services
{
    public class SevendayService : ISevendayService
    {
        private INetworkService networkService;
        private ICacheService cache;
        public SevendayService()
        {
            networkService = Ioc.Container.Resolve<INetworkService>();
            cache = Ioc.Container.Resolve<ICacheService>();
        }

        private async Task<string> getApiUrlAsync(string api)
        {
            var server = await getSelectedServerFromCache();
            return string.Format("http://{0}:{1}/{2}", server.Host, server.Port, api);
        }

        public async Task<SevenDays.Model.Entity.Server> getSelectedServerFromCache()
        {
            var serverKey = Settings.SevendaysSelectedServer;

            if (string.IsNullOrEmpty(serverKey))
                return null;

            return await cache.GetObject<SevenDays.Model.Entity.Server>(serverKey);
        }

        public async Task<bool> CanConnectToServer(string host, string port)
        {
            return await networkService.CanConnectToService(string.Format("http://{0}", host), port);
        }

        public async Task<bool> CanConnectToServer(SevenDays.Model.Entity.Server server)
        {
            if (server == null)
                return false;
            if (string.IsNullOrEmpty(server.Host) || string.IsNullOrEmpty(server.Port))
                return false;

            return await CanConnectToServer(server.Host, server.Port);
        }

        public async Task<bool> CanConnectToServer()
        {
            var server = await getSelectedServerFromCache();

            if (server == null)
                return false;

            return await CanConnectToServer(server);
        }

        public async Task<Response<Inventory>> GetPlayerInventory(long steamId)
        {
            var response = new Response<Inventory>();

            if (!await CanConnectToServer())
                return response;

            Insights.Track(string.Format("Getting player inventory for {0}", steamId));

            string url = string.Format("{0}steamId={1}", await getApiUrlAsync(ApiConstants.Seven.PlayerInventory), steamId);

            using (var handle = Insights.TrackTime("Seven_GetPlayerInventory"))
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var result = await client.GetStringAsync(url);
                response.Result = JsonConvert.DeserializeObject<Inventory>(result);
            }

            return response;
        }

        public async Task<ListResponse<Player>> GetPlayersLocation()
        {
            var response = new ListResponse<Player>();

            if (!await CanConnectToServer())
                return response;

            string url = await getApiUrlAsync(ApiConstants.Seven.PlayerLocation);

            using (var handle = Insights.TrackTime("Seven_GetPlayersLocation"))
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var result = await client.GetStringAsync(url);
                response.Result = JsonConvert.DeserializeObject<IEnumerable<Player>>(result);
            }

            return response;
        }

        public async Task<string> GetInventoryImageUrl(string item)
        {
            string url = await getApiUrlAsync(ApiConstants.Seven.InventoryImage);
            return string.Format(url, item);
        }

        public async Task<string> GetMapUrl()
        {
            return await getApiUrlAsync(ApiConstants.Seven.Map);
        }
    }
}
