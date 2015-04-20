using SevenDays.Model.Base;
using SevenDays.Model.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.Interfaces
{
    public interface ISteamService
    {
        Task<ListResponse<Player>> GetPlayerSummaries(params long[] steamIds);
        Task<Response<PlayerStats>> GetUserStatsForGame(long steamId);
        Task<Response<Game>> GetSchemaForGame(long steamId);
        Task<Response<PlayerStats>> GetPlayerAchievements(long steamId);
    }
}
