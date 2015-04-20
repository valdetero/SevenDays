using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;
using Connectivity.Plugin;

namespace SevenDays.Core.Services
{
    public class NetworkService : INetworkService
    {
        public async Task<bool> CanConnectToService(string host, string port = "")
        {
            int portInt;

            return CrossConnectivity.Current.IsConnected 
                && (int.TryParse(port, out portInt) 
                    ? await CrossConnectivity.Current.IsRemoteReachable(host, portInt)
                    : await CrossConnectivity.Current.IsReachable(host));
        }
    }
}
