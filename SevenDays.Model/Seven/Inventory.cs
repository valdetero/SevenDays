using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Seven
{
    public class Inventory
    {
        public IEnumerable<Item> Bag { get; set; }
        public IEnumerable<Item> Belt { get; set; }
    }
}
