using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;
using Plugin.Connectivity;

namespace SevenDays.Core.Services
{
    public class NetworkService : INetworkService
    {
        private readonly ILogger logger;
        public NetworkService()
        {
            logger = Ioc.Container.Resolve<ILogger>();
        }
        public async Task<bool> CanConnectToService(string host)
        {
            logger.Track(string.Format("Checking connectivity to {0}", host));
            return CrossConnectivity.Current.IsConnected
                && await CrossConnectivity.Current.IsReachable(host);
        }

        public async Task<bool> CanConnectToService(string host, string port)
        {
            int portInt;

            logger.Track(string.Format("Checking connectivity to {0}:{1}", host, port));
            return CrossConnectivity.Current.IsConnected
                && (int.TryParse(port, out portInt)
                && await CrossConnectivity.Current.IsRemoteReachable(host, portInt));
        }
    }
}
