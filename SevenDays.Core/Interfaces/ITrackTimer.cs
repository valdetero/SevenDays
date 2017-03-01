using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDays.Core.Interfaces
{
	public interface ITrackTimer : IDisposable
	{
		ITrackTimer Init(string identifier);
		void Start();
		void Stop();
	}
}
