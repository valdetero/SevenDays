using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.Interfaces
{
    public interface INetworkService
    {
        Task<bool> CanConnectToService(string host);
        Task<bool> CanConnectToService(string host, string port = "");
    }
}
