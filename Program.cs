using Avalonia;

using System;

using WifiAutoConnect.Models;

namespace WifiAutoConnect;

internal static class Program
{
	#region Methods
	[STAThread]
	public static void Main(string[] args)
	{
		var builder = BuildAvaloniaApp();
		_ = builder.StartWithClassicDesktopLifetime(args);
	}

	public static AppBuilder BuildAvaloniaApp()
	{
		return AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.LogToTrace();
	}
	#endregion
}