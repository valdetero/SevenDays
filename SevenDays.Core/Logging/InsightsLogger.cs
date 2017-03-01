using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;

namespace SevenDays.Core.Logging
{
    public class InsightsLogger : ILogger
    {
        public void LogException(Exception exception)
        {
            Xamarin.Insights.Report(exception);
        }

        public void Track(string trackIdentifier)
        {
            Xamarin.Insights.Track(trackIdentifier);
        }
    }

	public class InsightsTimer : ITrackTimer
	{
		Xamarin.ITrackHandle _handle;

		public ITrackTimer Init(string identifier)
		{
			_handle = Xamarin.Insights.TrackTime(identifier);

			return this;
		}
		public void Start()
		{
			_handle.Start();
		}

		public void Stop()
		{
			_handle.Stop();
		}

		public void Dispose()
		{
			this.Stop();
		}
	}
}
