using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SevenDays.UI
{
    public static class PlatformImage
    {
        public static string Resolver(string image)
        {
            return string.Format(Device.OnPlatform("{0}", "{0}", "Assets/{0}"), image);
        }
    }
}
