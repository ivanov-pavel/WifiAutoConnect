using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using WifiAutoConnect.Settings;
using WifiAutoConnect.ViewModels;
using WifiAutoConnect.Views;

namespace WifiAutoConnect;

public class App : Application
{
	#region Methods
	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		var settings = ApplicationSettings.LoadSettings() ?? ApplicationSettings.DefaultSettings;
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			var model = new MainWindowModel(settings);
			var window = new MainWindowView(model);
			desktop.MainWindow = window;
			desktop.Exit += (_, _) => settings.SaveSettings();
		}

		base.OnFrameworkInitializationCompleted();
	}
	#endregion
}