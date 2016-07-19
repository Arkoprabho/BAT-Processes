using System;
using System.Diagnostics;

namespace BATProcesses.PowerManagement
{
	/// <summary>
	/// Contains methods to switch to high performance battery mode.
	/// </summary>
	class BatteryMode
	{
		ProcessStartInfo psi;
		Process p;
		internal BatteryMode()
		{
			try
			{
				psi = new ProcessStartInfo("cmd.exe");
				p = Process.Start(psi);
				psi.RedirectStandardInput = true;
				psi.RedirectStandardOutput = true;
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				throw;
			}
		}
		/// <summary>
		/// Changes the power mode based on the boolean input
		/// true: High Performance
		/// false: Power Saver.
		/// </summary>
		/// <param name="status"></param>
		internal void PowerMode(bool status)
		{
			try
			{
				if (p != null)
				{
					if (status)
						p.StandardInput.WriteLine("powercfg -setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");
					else
						p.StandardInput.WriteLine("powercfg -setactive a1841308-3541-4fab-bc81-f71556f20b4a");
					p.StandardInput.Close();
				}
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				throw;
			}
			finally
			{
				p.StandardInput.Close();
			}
		}
	}
}
