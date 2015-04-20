using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MethodDecoratorInterfaces;

[module: SevenDays.Core.Helpers.Insights]

namespace SevenDays.Core.Helpers
{
    [AttributeUsage(
            AttributeTargets.Method
            | AttributeTargets.Constructor
            | AttributeTargets.Assembly
            | AttributeTargets.Module)]
    public class InsightsAttribute : Attribute, IMethodDecorator
    {
        private string _methodName;

        public void Init(object instance, MethodBase method, object[] args)
        {
            _methodName = //method.DeclaringType.FullName + "." + 
                method.Name;
        }

        public void OnEntry()
        {
            Xamarin.Insights.Track(string.Format("OnEntry: {0}", _methodName));
        }

        public void OnExit()
        {
            Xamarin.Insights.Track(string.Format("OnExit: {0}", _methodName));
        }

        public void OnException(Exception exception)
        {
            Xamarin.Insights.Report(exception);
        }
    }
}
