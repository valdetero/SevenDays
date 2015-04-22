using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SevenDays.Model.Steam
{
    public class Player
    {
        [JsonProperty("steamid")]
        public long SteamId { get; set; }
        [JsonProperty("personaname")]
        public string PersonaName { get; set; }
        [JsonProperty("profileurl")]
        public string ProfileUrl { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("avatarmedium")]
        public string AvatarMedium { get; set; }
        [JsonProperty("avatarfull")]
        public string AvatarFull { get; set; }
        [JsonProperty("personastate")]
        public PersonaState PersonaState { get; set; }
        [JsonProperty("lastlogoff")]
        public long LastLogOff { get; set; }
        [JsonProperty("communityvisibilitystate")]
        public CommunityVisibilityState CommunityVisibilityState { get; set; }
    }
}
