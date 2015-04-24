using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;
using Nunit = NUnit.Framework;

namespace SevenDays.Tests.Shared
{
    public class NetworkServiceTests
    {
        [Xunit.Fact]
        [Nunit.Test]
#if !WINDOWS_PHONE
        [Nunit.Timeout(Int32.MaxValue)]
#endif
        public async void CheckNetworkConnectivity()
        {
            var service = SevenDays.Core.Ioc.Container.Resolve<INetworkService>();

            var result = await service.CanConnectToService("www.google.com");

            Assert.True(result);
        }
    }
}
