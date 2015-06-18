using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;

namespace SevenDays.Tests.Shared
{
    public class NetworkServiceMock : INetworkService
    {
        public Task<bool> CanConnectToService(string host)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CanConnectToService(string host, string port)
        {
            return Task.FromResult(true);
        }
    }
}
