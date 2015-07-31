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
            Container.Register<ILogger>(() => new LoggerMock());
            Container.Register<INetworkService>(() => new NetworkService());            
        }
        /*
         * Connectivity plugin in xUnit tests has issues on 
         *      Windows Phone - throws UnauthorizedAccessException: Invalid cross-thread access
         *      Unit/VS Runner - it has no implementation in the PCL
         */
#if !WINDOWS_PHONE && __MOBILE__
        [Theory]
        [InlineData("seth-7dtd.cloudapp.net","8082")]
        [InlineData("home.wtfnext.com","26903")]
        public async void CheckNetworkConnectivity(string host, string port)
        {
            var service = Container.Resolve<INetworkService>();

            var result = await service.CanConnectToService(host, port);

            Assert.True(result);
        }
#endif
    }
}
