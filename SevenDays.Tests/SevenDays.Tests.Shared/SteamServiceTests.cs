using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using SevenDays.Core.Services;
using Xunit;

namespace SevenDays.Tests.Shared
{
    public class SteamServiceTests
    {
        public SteamServiceTests()
        {
            Container.Clear();
            Container.Register<ILogger>(() => new LoggerMock());
            Container.Register<INetworkService>(new NetworkServiceMock());
            Container.Register<ISteamService>(new SteamService());
        }

        [Fact]
        public async Task GetPlayerSummaries()
        {
            var service = Container.Resolve<ISteamService>();

            var result = await service.GetPlayerSummaries(76561197968329571);

            Assert.True(result.Successful);
            Assert.True(result.Result.Any());
            Assert.Equal(result.Result.First().PersonaName, "svickn");
        }

        [Fact]
        public async Task GetPlayerSummaries_2()
        {
            var service = Container.Resolve<ISteamService>();

            var result = await service.GetPlayerSummaries(76561197968329571, 76561198021070820);

            Assert.True(result.Successful);
            Assert.True(result.Result.Any());
            Assert.NotNull(result.Result.SingleOrDefault(x => x.PersonaName == "svickn"));
            Assert.NotNull(result.Result.SingleOrDefault(x => x.PersonaName == "yo momma"));
        }
    }
}
