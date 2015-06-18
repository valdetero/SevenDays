using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using SevenDays.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using SevenDays.Core.Helpers;

namespace SevenDays
{
    public static class IoC
    {
        public static void Init()
        {
            Container.Register<INetworkService>(() => new NetworkService());
            Container.Register<ISteamService>(() => new SteamService());
            Container.Register<ISevendayService>(() => new SevendayService());
            Container.Register<ISettings>(() => new Settings());
            Container.Register<ICacheService>(() => new AkavacheCacheService());
        }
    }
}
