using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.Helpers
{
    public static class InitialData
    {
        public static IList<Model.Entity.Server> GetServers()
        {
            var data = new List<Model.Entity.Server>
            {
                new Model.Entity.Server("seth-7dtd.cloudapp.net", "8082"),
                new Model.Entity.Server("misko-7dtd.cloudapp.net", "8082"),
                new Model.Entity.Server("home.wtfnext.com", "26903"),
                new Model.Entity.Server("68.232.165.121", "25004"),
            };

            return data;
        }
    }
}
