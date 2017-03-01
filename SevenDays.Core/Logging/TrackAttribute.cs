using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MethodDecoratorInterfaces;
using SevenDays.Core.Interfaces;

namespace SevenDays.Core.Logging
{
    [AttributeUsage(
            AttributeTargets.Method
            | AttributeTargets.Constructor
            | AttributeTargets.Assembly
            | AttributeTargets.Module)]
    public class TrackAttribute : Attribute, IMethodDecorator
    {
		private readonly ILogger _logger;
        private string _methodName;

		public TrackAttribute()
		{
			_logger = Ioc.Container.Resolve<ILogger>();
		}

        public void Init(object instance, MethodBase method, object[] args)
        {
            _methodName = //method.DeclaringType.FullName + "." +
                method.Name;
		}

		public void OnEntry()
        {
			_logger.Track($"OnEntry: {_methodName}");
        }

        public void OnExit()
        {
			_logger.Track($"OnExit: {_methodName}");
        }

        public void OnException(Exception exception)
        {
			_logger.LogException(exception);
        }
    }
}
