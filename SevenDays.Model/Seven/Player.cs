using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Seven
{
    public class Player
    {
        public Player()
        {
            Position = new Position();
            Inventory = new Inventory();
        }
        public long SteamId { get; set; }
        [JsonProperty("ip")]
        public string IpAddress { get; set; }
        public string Name { get; set; }
        [JsonProperty("online")]
        public bool IsOnline { get; set; }
        public Position Position { get; set; }
        public Inventory Inventory { get; set; }
    }
}
