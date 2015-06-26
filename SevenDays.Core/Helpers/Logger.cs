using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;

namespace SevenDays.Core.Helpers
{
    public class Logger : ILogger
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
}
