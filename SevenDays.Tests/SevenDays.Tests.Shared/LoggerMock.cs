using System;
using System.Collections.Generic;
using System.Text;
using SevenDays.Core.Interfaces;

namespace SevenDays.Tests
{
    public class LoggerMock : ILogger
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
            //System.Diagnostics.Debug.WriteLine($"Tracked: {trackIdentifier}");
            System.Diagnostics.Debug.WriteLine(string.Format("Tracked: {0}", trackIdentifier));
        }
    }
}
