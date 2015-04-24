using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;
using Nunit = NUnit.Framework;

namespace SevenDays.Tests.Shared
{
    public class SteamServiceTests
    {
        [Xunit.Fact]
        [Nunit.Test]
#if !WINDOWS_PHONE
        [Nunit.Timeout(Int32.MaxValue)]
#endif
        public async Task GetPlayerSummaries()
        {
            var service = SevenDays.Core.Ioc.Container.Resolve<ISteamService>();

            var result = await service.GetPlayerSummaries(76561197968329571);

            Assert.True(result.Successful);
            Assert.True(result.Result.Any());
            Assert.AreEqual(result.Result.First().PersonaName, "svickn");
        }

        [Xunit.Fact]
        [Nunit.Test]
#if !WINDOWS_PHONE
        [Nunit.Timeout(Int32.MaxValue)]
#endif
        public async Task GetPlayerSummaries_2()
        {
            var service = SevenDays.Core.Ioc.Container.Resolve<ISteamService>();

            var result = await service.GetPlayerSummaries(76561197968329571, 76561198021070820);

            Assert.True(result.Successful);
            Assert.True(result.Result.Any());
            Assert.NotNull(result.Result.SingleOrDefault(x => x.PersonaName == "svickn"));
            Assert.NotNull(result.Result.SingleOrDefault(x => x.PersonaName == "yo momma"));
        }

        //[Xunit.Fact]
        //public async Task GetPlayerSummaries_X()
        //{
        //    var service = SevenDays.Core.Ioc.Container.Resolve<ISteamService>();

        //    var result = await service.GetPlayerSummaries(76561197968329571);

        //    Assert.True(result.Successful);

        //}
        //[Nunit.Test, Nunit.Timeout(Int32.MaxValue)]
        //public async Task GetPlayerSummaries_N()
        //{
        //    var service = SevenDays.Core.Ioc.Container.Resolve<ISteamService>();

        //    var result = await service.GetPlayerSummaries(76561197968329571);

        //    Assert.True(result.Successful);
        //}
    }
}
