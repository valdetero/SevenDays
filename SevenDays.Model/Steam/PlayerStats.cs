using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Steam
{
    public class PlayerStats
    {
        public long SteamId { get; set; }
        public string GameName { get; set; }
    }
}
