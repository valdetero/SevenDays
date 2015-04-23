using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        public SevendayService()
        {
            networkService = Ioc.Container.Resolve<INetworkService>();
        }

        private string getApiUrl(string api)
        {
            return string.Format("http://{0}:{1}/{2}", Settings.SevendaysServer, Settings.SevendaysPort, api);
        }

        private async Task<bool> canConnectToServer()
        {
            return await networkService.CanConnectToService(string.Format("http://{0}", Settings.SevendaysServer), Settings.SevendaysPort);
        }

        public async Task<bool> CanConnectToServer()
        {
            return await canConnectToServer();
        }

        public async Task<Response<Inventory>> GetPlayerInventory(long steamId)
        {
            var response = new Response<Inventory>();

            if (!await canConnectToServer())
                return response;

            Insights.Track(string.Format("Getting player inventory for {0}", steamId));

            string url = string.Format("{0}steamId={1}", getApiUrl(ApiConstants.Seven.PlayerInventory), steamId);

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

            if (!await canConnectToServer())
                return response;

            string url = getApiUrl(ApiConstants.Seven.PlayerLocation);

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var result = await client.GetStringAsync(url);
                response.Result = JsonConvert.DeserializeObject<IEnumerable<Player>>(result);
            }

            return response;
        }

        public string GetInventoryImageUrl(string item)
        {
            string url = getApiUrl(ApiConstants.Seven.InventoryImage);
            return string.Format(url, item);
        }

        public string GetMapUrl()
        {
            return getApiUrl(ApiConstants.Seven.Map);
        }
    }
}
