using System;
using BATProcesses.Hotspot;

namespace BATProcesses
{
	class Program
	{
		static void Main(string [] args)
		{
			HotspotDetails hd = new HotspotDetails();
			hd.GetData();
			Console.WriteLine("Press any key to exit...");
			Console.ReadLine();
		}
	}
}
