using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Steam
{
    public class Game
    {
        [JsonProperty("gameName")]
        public string Name { get; set; }
        [JsonProperty("gameVersion")]
        public string Version { get; set; }
        [JsonProperty("availableGameStats")]
        public object Stats { get; set; }
    }
}
