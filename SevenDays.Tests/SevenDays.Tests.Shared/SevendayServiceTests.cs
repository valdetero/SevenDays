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

namespace SevenDays.Tests.Shared
{
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

        [Fact]
        public async Task GetPlayerInventory()
        {
            var service = Container.Resolve<ISevendayService>();

            var result = await service.GetPlayerInventory(76561197968329571);

            Assert.True(result.Successful, result.Message);
            Assert.NotNull(result.Result.Bag);
            Assert.NotNull(result.Result.Belt);
        }

        [Fact]
        public async Task GetPlayersLocation()
        {
            var service = Container.Resolve<ISevendayService>();

            var result = await service.GetPlayersLocation();

            Assert.True(result.Successful, result.Message);
            Assert.NotNull(result.Result);
            Assert.True(result.Result.Any(x => x.Name == "svickn"), "No player by the name of svickn");
        }
    }
}
