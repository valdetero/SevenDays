using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model
{
    public static class AutomapperExtension
    {
        public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }
    }
}
