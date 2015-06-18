using System;
using System.Collections.Generic;
using System.Text;
using SevenDays.Core.Interfaces;

namespace SevenDays.Tests.Shared
{
    public class SettingsMock : ISettings
    {
        public SettingsMock()
        {
            this.SevendaysSelectedServer = "home.wtfnext.com:26903";
        }

        public string SevendaysSelectedServer { get; set; }
    }
}
