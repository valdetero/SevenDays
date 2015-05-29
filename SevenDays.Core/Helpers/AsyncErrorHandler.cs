using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.Helpers
{
    public class AsyncErrorHandler
    {
        public static void HandleException(Exception exception)
        {
            Xamarin.Insights.Report(exception);
        }
    }
}
