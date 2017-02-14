using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Seven
{
	public class Equipment
	{
		public Item Head { get; set; }
		public Item Eyes { get; set; }
		public Item Face { get; set; }
		public Item Armor { get; set; }
		public Item Jacket { get; set; }
		public Item Shirt { get; set; }
		public Item LegArmor { get; set; }
		public Item Pants { get; set; }
		public Item Boots { get; set; }
		public Item Gloves { get; set; }

		public List<Item> ToItemList()
		{
			var list = new List<Item>();

			if (Head != null)
				list.Add(Head);
			if (Eyes != null)
				list.Add(Eyes);
			if (Face != null)
				list.Add(Face);
			if (Armor != null)
				list.Add(Armor);
			if (Jacket != null)
				list.Add(Jacket);
			if (Shirt != null)
				list.Add(Shirt);
			if (LegArmor != null)
				list.Add(LegArmor);
			if (Pants != null)
				list.Add(Pants);
			if (Boots != null)
				list.Add(Boots);
			if (Gloves != null)
				list.Add(Gloves);
			return list;
		}
	}
}
