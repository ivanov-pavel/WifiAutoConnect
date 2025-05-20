// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WifiAutoConnect.Models;

public static class WifiManager
{
	#region Methods
	public static bool IsNetworkConnected(string ssid)
	{
		try
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "netsh",
					Arguments = "wlan show interfaces",
					RedirectStandardOutput = true,
					UseShellExecute = false,
					CreateNoWindow = true
				}
			};

			process.Start();
			var output = process.StandardOutput.ReadToEnd().AsSpan();
			process.WaitForExit();

			var flag = false;
			var split = output.Split('\n');
			foreach (var range in split)
			{
				var line = output[range].Trim();
				if (line.IsEmpty)
				{
					flag = false;
					continue;
				}

				switch (flag)
				{
					case false when line.StartsWith("State"):
					{
						var index = line.IndexOf(':') + 1;
						var value = line[index..].Trim();
						if (value is "connected")
							flag = true;
						continue;
					}

					case true when line.StartsWith("SSID"):
					{
						var index = line.IndexOf(':') + 1;
						var value = line[index..].Trim();
						if (ssid.CompareTo(value, StringComparison.Ordinal) == 0)
							return true;

						flag = false;
						break;
					}
				}
			}
		}
		catch (Exception exception)
		{
			Debug.WriteLine(exception);
		}
		return false;
	}

	public static IReadOnlyList<string> GetAvailableNetworks()
	{
		try
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "netsh",
					Arguments = "wlan show networks",
					RedirectStandardOutput = true,
					UseShellExecute = false,
					CreateNoWindow = true
				}
			};

			process.Start();
			var output = process.StandardOutput.ReadToEnd().AsSpan();
			process.WaitForExit();

			var split = output.Split('\n');
			var networks = new List<string>();
			foreach (var range in split)
			{
				var line = output[range].Trim();
				if (line.IsEmpty)
					continue;

				if (!line.StartsWith("SSID"))
					continue;

				var index = line.IndexOf(':') + 1;
				var ssid = line[index..].Trim();
				if (!ssid.IsEmpty)
					networks.Add(ssid.ToString());
			}

			return networks;
		}
		catch (Exception exception)
		{
			Debug.WriteLine(exception);
			return [];
		}
	}

	public static bool ConnectToNetwork(string ssid)
	{
		try
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "netsh",
					Arguments = $"wlan connect name=\"{ssid}\"",
					RedirectStandardOutput = true,
					UseShellExecute = false,
					CreateNoWindow = true
				}
			};
			process.Start();
			process.WaitForExit();
			return process.ExitCode == 0;
		}
		catch (Exception exception)
		{
			Debug.WriteLine(exception);
			return false;
		}
	}
	#endregion
}