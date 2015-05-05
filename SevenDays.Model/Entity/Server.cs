using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Model.Entity
{
    public class Server
    {
        public string Host { get; set; }
        public string Port { get; set; }

        public Server()
        {

        }

        public Server(string host, string port) :this()
        {
            this.Host = host;
            this.Port = port;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", this.Host, this.Port);
        }
    }
}
