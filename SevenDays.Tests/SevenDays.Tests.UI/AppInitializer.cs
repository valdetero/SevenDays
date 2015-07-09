using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using SevenDays.Core;

namespace SevenDays.Tests.UI
{
	public class AppInitializer
	{
		public static IApp StartApp(Platform platform)
		{
			// TODO: If the iOS or Android app being tested is included in the solution 
			// then open the Unit Tests window, right click Test Apps, select Add App Project
			// and select the app projects that should be tested.
			if(platform == Platform.Android) {
				return ConfigureApp
					.Android
					.Debug()
					.EnableLocalScreenshots()
					.ApiKey(ApiConstants.TestCloud.Key)

					//Device
					//.DeviceSerial()
					//.InstalledApp("SevenDays.SevenDays")

					//Simulator
					.ApkFile ("../../../../SevenDays.UI/SevenDays.UI.Droid/bin/Debug/SevenDays.SevenDays.apk")

					.StartApp();
			}

			return ConfigureApp
				.iOS
				.Debug()
				.EnableLocalScreenshots()
				.ApiKey(ApiConstants.TestCloud.Key)

				//Device
				//.DeviceIdentifier("42a46071aaf6b895aebc4f9ace29972270dac022")
				//.InstalledApp("com.sparkhound.SevenDays.UI")

				//Simulator
				.AppBundle ("../../../../SevenDays.UI/SevenDays.UI.iOS/bin/iPhoneSimulator/Debug/SevenDaysUIiOS.app")

				.StartApp();
		}
	}
}

