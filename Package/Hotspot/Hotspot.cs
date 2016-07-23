using System;
using System.Text;
using System.Diagnostics;

// Works fine. Check if internet access is available.
namespace BATProcesses.Hotspot
{
	/// <summary>
	/// Contains methods to generate a random password.
	/// </summary>
	class Password
	{
		Random r;
		internal Password()
		{
			try
			{
				r = new Random();
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				// HACK rethrows an exception.
				throw;
			}
		}
		/// <summary>
		/// Returns a random number between the specified interval.
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		int RandomNumber(int min, int max)
		{
			try
			{
				return r.Next(min, max);
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				// HACK rethrows an exception.
				throw e;
			}
		}

		/// <summary>
		/// Returns a random string of the specified size.
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		string RandomString(int size)
		{
			try
			{
				StringBuilder sb = new StringBuilder();
				char ch;
				for (int i = 0;i < size;i++)
				{
					ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * r.NextDouble() + 65)));
					sb.Append(ch);
				}
				return sb.ToString();
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				// HACK rethrows an exception.
				throw;
			}
		}

		protected string GetRandomPassword()
		{
			StringBuilder sb;
			try
			{
				sb = new StringBuilder();
				sb.Append(RandomString(6));
				sb.Append(RandomNumber(10, 99));
				return sb.ToString();
			}
			catch (Exception e)
			{
				Console.Write(e.Message);
				// HACK rethrows an exception.
				throw;
			}
		}
		protected string GetPassword()
		{
			try
			{
				string password = Console.ReadLine();
				if (password.Length < 8)
				{
					Console.Write("Password too short! Try again");
					GetPassword();
				}
				return password;
			}
			catch (StackOverflowException e)
			{
				Console.Write(e.Message);
				Console.WriteLine("Terminating process!");
				// Exit Code 1 indicating exception in GetPassword method.
				Environment.Exit(1);
				// HACK rethrows an exception.
				throw;
			}
		}
	}

	class HotspotDetails : Password
	{
		string name, password;
		Hotspot h;
		internal HotspotDetails()
		{
			h = new Hotspot();
		}

		internal void GetData()
		{
			try
			{
				Console.Write("Use previous settings\n1.Yes\n2.No");
				int choice = int.Parse(Console.ReadLine());
				if (choice == 1)
					h.StartHotspot();
				else
				{
					Console.Write("Enter SSID: ");
					name = Console.ReadLine();
					Console.Write("1.Random password\n2.Fixed password");
					choice = int.Parse(Console.ReadLine());
					if (choice == 1)
						password = GetRandomPassword();
					else
					{
						password = GetPassword();
					}
					Console.WriteLine("Your password is: {0}", password);
					h.GetHotspot(name, password);
				}
			}
			catch (StackOverflowException e)
			{
				Console.Write(e.Message);
				Console.Write("Terminating process!");
				// Exit Code 2 indicating exception in GetData method.
				Environment.Exit(2);
			}
			catch (Exception e)
			{
				Console.Write(e.Message + "\nTry again");
				GetData();
			}
		}
	}

	class Hotspot
	{
		ProcessStartInfo psi;
		Process p;
		internal Hotspot()
		{
			try
			{
				psi = new ProcessStartInfo("C:\Windows\System32\cmd.exe");
				psi.RedirectStandardInput = true;
				psi.RedirectStandardOutput = true;
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;
				p = Process.Start(psi);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				// TODO add code to retry creating the above functions.
				// HACK rethrows an exception.
				throw;
			}
		}

		internal void GetHotspot(string ssid, string password)
		{
			try
			{
				p.StandardInput.WriteLine("netsh wlan set hostednetwork mode=allow");
				p.StandardInput.WriteLine("netsh wlan set hostednetwork ssid=" + ssid + " key=" + password);
				StartHotspot();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				// TODO add code to retry creating the above functions.
				// HACK rethrows exception.
				throw;
			}
		}
		internal void StartHotspot()
		{
			try
			{
				p.StandardInput.WriteLine("netsh wlan set hostednetwork mode=allow");
				p.StandardInput.WriteLine("netsh wlan start hostednetwork");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				// TODO add code to retry creating the above functions.
				// HACK rethrows an exception.
				throw;
			}
		}

		internal void StopHostedNetwork()
		{
			try
			{
				p.StandardInput.WriteLine("netsh wlan stop hostednetwork");
				p.StandardInput.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				// TODO add code to retry creating the above functions.
				// HACK rethrows an exception.
				throw;
			}
		}
	}
}
