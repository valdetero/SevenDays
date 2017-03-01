using SevenDays.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace SevenDays.Core.Logging
{
	public class MobileCenterLogger : ILogger
	{
		public void LogException(Exception exception)
		{
			MobileCenterLog.Error(MobileCenterLog.LogTag, exception.Message, exception);
		}

		public void Track(string trackIdentifier)
		{
			Analytics.TrackEvent(trackIdentifier);
			MobileCenterLog.Info(MobileCenterLog.LogTag, trackIdentifier);
		}
	}
}
