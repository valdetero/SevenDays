using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.Interfaces
{
    public interface ILogger
    {
        void LogException(Exception exception);
        void Track(string trackIdentifier);
    }
}
