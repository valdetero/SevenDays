using SevenDays.Model.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Steam
{
    public class PlayerSummaryDto
    {
        public PlayerSummaryDto()
        {
            Response = new PlayerSummaryResponse();
        }
        public PlayerSummaryResponse Response { get; set; }
    }

    public class PlayerSummaryResponse
    {
        public PlayerSummaryResponse()
        {
            Players = new List<Steam.Player>();
        }

        public IEnumerable<Steam.Player> Players { get; set; }
    }
}
