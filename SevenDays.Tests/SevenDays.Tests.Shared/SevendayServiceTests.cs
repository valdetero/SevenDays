using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using Nunit = NUnit.Framework;

namespace SevenDays.Tests.Shared
{
    public class SevendayServiceTests
    {
        [Xunit.Fact]
        [Nunit.Test, Nunit.Timeout(Int32.MaxValue)]
        public async Task GetPlayerInventory()
        {
            Settings.SevendaysServer = "home.wtfnext.com";
            Settings.SevendaysPort = "26903";
            var service = SevenDays.Core.Ioc.Container.Resolve<ISevendayService>();

            var result = await service.GetPlayerInventory(76561197968329571);

            Assert.True(result.Successful);
            Assert.NotNull(result.Result.Bag);
            Assert.NotNull(result.Result.Belt);
        }

        [Xunit.Fact]
        [Nunit.Test, Nunit.Timeout(Int32.MaxValue)]
        public async Task GetPlayersLocation()
        {
            Settings.SevendaysServer = "home.wtfnext.com";
            Settings.SevendaysPort = "26903";
            var service = SevenDays.Core.Ioc.Container.Resolve<ISevendayService>();

            var result = await service.GetPlayersLocation();

            Assert.True(result.Successful);
            Assert.NotNull(result.Result);
            Assert.True(result.Result.Any(x => x.Name == "svickn"));
        }
    }
}
