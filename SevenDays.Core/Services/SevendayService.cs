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
		private bool firstrun = true;
        private readonly INetworkService networkService;
        private readonly ICacheService cache;
        private readonly ISettings settings;
        private readonly ILogger logger;
        public SevendayService()
        {
            networkService = Ioc.Container.Resolve<INetworkService>();
            cache = Ioc.Container.Resolve<ICacheService>();
            settings = Ioc.Container.Resolve<ISettings>();
            logger = Ioc.Container.Resolve<ILogger>();
        }

        private async Task<string> getApiUrlAsync(string api)
        {
            var server = await getSelectedServerFromCache();
            return $"http://{server.Host}:{server.Port}/{api}adminuser={ApiConstants.Seven.User}&admintoken={ApiConstants.Seven.Token}";
        }

        public async Task<SevenDays.Model.Entity.Server> getSelectedServerFromCache()
        {
#if DEBUG
			if(firstrun)
			{
				var server = new SevenDays.Model.Entity.Server("seth-7dtd.cloudapp.net", "8082");
				if(string.IsNullOrEmpty(settings.SevendaysSelectedServer))
					settings.SevendaysSelectedServer = server.ToString();
				if(await cache.GetObject<SevenDays.Model.Entity.Server>(server.ToString()) == null)
					await cache.InsertObject(server.ToString(), server);

				firstrun = false;
			}
#endif

            var serverKey = settings.SevendaysSelectedServer;

            if (string.IsNullOrEmpty(serverKey))
                return null;

            return await cache.GetObject<SevenDays.Model.Entity.Server>(serverKey);
        }

        public Task<bool> CanConnectToServer(string host, string port)
        {
            return networkService.CanConnectToService(string.Format("http://{0}", host), port);
        }

        public Task<bool> CanConnectToServer(SevenDays.Model.Entity.Server server)
        {
            if (server == null)
                return Task.FromResult(false);
            if (string.IsNullOrEmpty(server.Host) || string.IsNullOrEmpty(server.Port))
                return Task.FromResult(false);

            return CanConnectToServer(server.Host, server.Port);
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

            string url = string.Format("{0}&steamId={1}", await getApiUrlAsync(ApiConstants.Seven.PlayerInventory), steamId);

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

            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    var result = await client.GetStringAsync(url);
                    response.Result = JsonConvert.DeserializeObject<IEnumerable<Player>>(result);
                }
            }
            /*
                System.Net.WebExceptionStatus.ReceiveFailure = 3
                Azure throws this error. However this enum doesn't exist in the namespace.
            */
            catch (System.Net.WebException ex)// when ((int)ex.Status == 3)
            {
                if ((int) ex.Status != 3)
                    throw;
                logger.LogException(ex);
            }

            return response;
        }

        public async Task<string> GetInventoryImageUrl(string item, string color)
        {
            string url = await getApiUrlAsync(ApiConstants.Seven.InventoryImage);
            return string.Format(url, item, color);
        }

        public async Task<string> GetMapUrl()
        {
            return await getApiUrlAsync(ApiConstants.Seven.Map);
        }
    }
}
