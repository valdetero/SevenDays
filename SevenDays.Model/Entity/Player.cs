using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model
{
    public class Player
    {
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public string Avatar { get; set; }
        public string SteamAvailability { get; set; }
        public long SteamId { get; set; }
        public DateTime LastLogOff { get; set; }
    }
}
