using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation;
using UIKit;

using Xunit.Runner;
using Xunit.Sdk;

namespace SevenDays.Tests.iOS
{
    [Register("AppDelegateXUnit")]
    public partial class AppDelegateXUnit : RunnerAppDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Bootstrapper.Setup();

            AddExecutionAssembly(typeof(ExtensibilityPointFactory).Assembly);

            AddTestAssembly(Assembly.GetExecutingAssembly());

            return base.FinishedLaunching(app, options);
        }
    }
}