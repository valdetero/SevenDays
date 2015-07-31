using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using SevenDays.Core.Services;
using SevenDays.Model.Base;
using SevenDays.Model.Seven;
using Xunit;

namespace SevenDays.Tests
{
    [Trait("Api", "SevenDay")]
    public class SevendayServiceTests
    {
        public SevendayServiceTests()
        {
            Container.Clear();
            Container.Register<INetworkService>(() => new NetworkServiceMock());
            Container.Register<ICacheService>(() => new CacheServiceMock());
            Container.Register<ISettings>(() => new SettingsMock());
            Container.Register<ILogger>(() => new LoggerMock());
            Container.Register<ISevendayService>(() => new SevendayService());
        }

        [Theory]
        [InlineData("seth-7dtd.cloudapp.net:8082", 76561198021070820)]
        [InlineData("home.wtfnext.com:26903", 76561197968329571)]
        public async Task GetPlayerInventory(string server, long steamId)
        {
            Container.Resolve<ISettings>().SevendaysSelectedServer = server;

            var service = Container.Resolve<ISevendayService>();

            var result = await service.GetPlayerInventory(steamId);

            Assert.True(result.Successful, result.Message);
            Assert.NotNull(result.Result.Bag);
            Assert.NotNull(result.Result.Belt);
        }

        [Theory]
        [InlineData("seth-7dtd.cloudapp.net:8082")]
        [InlineData("home.wtfnext.com:26903")]
        public async Task GetPlayersLocation(string server)
        {
            Container.Resolve<ISettings>().SevendaysSelectedServer = server;

            var service = Container.Resolve<ISevendayService>();

            var result = await service.GetPlayersLocation();

            Assert.True(result.Successful, result.Message);
            Assert.NotNull(result.Result);
            Assert.True(result.Result.Any(), "No player on the server");
        }
    }
}
