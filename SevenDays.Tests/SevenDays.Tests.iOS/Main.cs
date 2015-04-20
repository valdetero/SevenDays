using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace SevenDays.Tests.iOS
{
    public class Application
    {
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegateXUnit");
            //UIApplication.Main(args, null, "AppDelegateNUnit");
        }
    }
}