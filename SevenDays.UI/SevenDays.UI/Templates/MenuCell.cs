using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SevenDays.UI.Templates
{
    public class MenuCell : ImageCell
    {
        public MenuCell() : base()
        {
            TextColor = Color.FromHex("AAAAAA");
            if(Device.OS == TargetPlatform.WinPhone)
                Height = 40;
        }
    }
}
