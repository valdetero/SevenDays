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
        Task<ListResponse<Player>> GetPlayersLocation();
        Task<Response<Inventory>> GetPlayerInventory(long steamId);
        string GetInventoryImageUrl(string item);
    }
}
