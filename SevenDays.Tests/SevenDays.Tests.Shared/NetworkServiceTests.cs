using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using SevenDays.Core.Services;
using Xunit;

namespace SevenDays.Tests.Shared
{
    public class NetworkServiceTests
    {
        public NetworkServiceTests()
        {
            Container.Clear();
            Container.Register<INetworkService>(() => new NetworkService());
        }

        [Fact]
        public async void CheckNetworkConnectivity()
        {
            var service = Container.Resolve<INetworkService>();

            var result = await service.CanConnectToService("www.google.com");

            Assert.True(result);
        }
    }
}
