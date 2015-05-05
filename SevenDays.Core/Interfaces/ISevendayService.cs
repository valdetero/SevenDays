using SevenDays.Model.Base;
using SevenDays.Model.Seven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.Interfaces
{
    public interface ISevendayService
    {
        Task<bool> CanConnectToServer();
        Task<bool> CanConnectToServer(SevenDays.Model.Entity.Server server);
        Task<bool> CanConnectToServer(string host, string port);
        Task<ListResponse<Player>> GetPlayersLocation();
        Task<Response<Inventory>> GetPlayerInventory(long steamId);
        Task<string> GetInventoryImageUrl(string item);
        Task<string> GetMapUrl();
    }
}
