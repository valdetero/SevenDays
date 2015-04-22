using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Mapper
{
    public static class AutoMapperConfig
    {
        public static void Register()
        {
            AutoMapper.Mapper.Reset();

            AutoMapper.Mapper.CreateMap<Steam.Player, Player>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.PersonaName))
                .ForMember(dest => dest.Avatar, opts => opts.MapFrom(src => src.AvatarMedium))
                .ForMember(dest => dest.LastLogOff, opts => opts.MapFrom(src => UnixTimeStampToDateTime(src.LastLogOff)))
                .ForMember(dest => dest.SteamAvailability, opts => opts.MapFrom(src => 
                    src.CommunityVisibilityState == Steam.CommunityVisibilityState.Private ? "Private" : src.PersonaState.ToString()));
            AutoMapper.Mapper.CreateMap<Seven.Player, Player>()
                .ForMember(dest => dest.SteamId, opts => opts.MapFrom(src => src.SteamId));
        }
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
