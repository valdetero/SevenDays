using SevenDays.Core.Helpers;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;
using SevenDays.Core.Logging;
using SevenDays.Core.Services;

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
            Container.Register<ILogger>(() => new MobileCenterLogger());
            Container.Register<ITrackTimer>(() => new StopWatch());
		}
    }
}
