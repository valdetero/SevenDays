using SevenDays.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.Logging
{
	public class ConsoleLogger : ILogger
	{
		public void LogException(Exception exception)
		{
			if (exception != null)
			{
				System.Diagnostics.Debug.WriteLine(exception.Message);
				while (exception.InnerException != null)
				{
					exception = exception.InnerException;
					System.Diagnostics.Debug.WriteLine(exception.Message);
				}
			}
		}

		public void Track(string trackIdentifier)
		{
			System.Diagnostics.Debug.WriteLine($"Tracked: {trackIdentifier}");
		}
	}

	public class StopWatch : ITrackTimer
	{
		System.Diagnostics.Stopwatch watch;
		string identifier;

		public ITrackTimer Init(string identifier)
		{
			this.identifier = identifier;
			watch = new System.Diagnostics.Stopwatch();
			this.Start();

			return this;
		}

		public void Dispose()
		{
			this.Stop();
		}

		public void Start()
		{
			watch.Restart();
		}

		public void Stop()
		{
			watch.Stop();
			string text = $"{identifier}: {watch.ElapsedMilliseconds} ms";
			System.Diagnostics.Debug.WriteLine(text);
#if TEST
			//Container.Resolve<Xunit.Abstractions.ITestOutputHelper>().WriteLine(text);
#endif
		}
	}
}
