using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Mapper
{
    public static class PlayerMapper
    {
        public static Player Map(Steam.Player steam, Seven.Player seven)
        {
            return AutoMapper.Mapper.Map<Player>(steam).Map(seven);
        }
    }
}
