using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenDays.Core.Interfaces;
using SevenDays.Core.Ioc;

namespace SevenDays.Core.ViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class InventoryViewModel : IViewModel
    {
        private ISevendayService sevendayService;
        public InventoryViewModel()
        {
            sevendayService = Container.Resolve<ISevendayService>();
        }

        public InventoryViewModel(Model.Seven.Item item, string pack):this()
        {
            Name = item.Name;
			IconName = item.Icon;
			IconColor = item.IconColor;
            Count = item.Count;
            Pack = pack;
        }

        public async Task Init()
        {
            Image = await sevendayService.GetInventoryImageUrl(IconName, IconColor);
        }

        public string Pack { get; set; }
        public string Name { get; set; }
		public string IconName { get; set; }
		public string IconColor { get; set; }
        public int Count { get; set; }
        public string Image { get; set; }
    }
}
