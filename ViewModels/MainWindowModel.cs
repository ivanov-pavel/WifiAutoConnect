using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Threading;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WifiAutoConnect.Models;
using WifiAutoConnect.Settings;

namespace WifiAutoConnect.ViewModels;

public partial class MainWindowModel : ViewModelBase
{
	#region Fields
	private DispatcherTimer? _updateTimer;
	private readonly ApplicationSettings _applicationSettings;
	#endregion

	#region Properties
	[ObservableProperty]
	public partial bool IsStarted { get; set; }

	[ObservableProperty]
	public partial string WifiSsid { get; set; }

	[ObservableProperty]
	public partial WifiState WifiState { get; set; }

	[ObservableProperty]
	public partial IEnumerable<string> AvailableSsids { get; private set; }
	#endregion

	#region Constructors
	public MainWindowModel(ApplicationSettings applicationSettings)
	{
		_applicationSettings = applicationSettings;

		WifiSsid = applicationSettings.NetworkSsid;
		AvailableSsids = WifiManager.GetAvailableNetworks();
	}
	#endregion

	#region Methods
	[RelayCommand]
	private void StartUpdate()
	{
		_updateTimer?.Stop();
		_updateTimer = null;

		var updateInterval = _applicationSettings.UpdateInterval;
		if (updateInterval > 0)
		{
			_updateTimer = new DispatcherTimer(TimeSpan.FromSeconds(updateInterval), DispatcherPriority.Background, OnTimerUpdate);

			IsStarted = true;
		}
	}

	[RelayCommand]
	private void StopUpdate()
	{
		_updateTimer?.Stop();
		_updateTimer = null;

		IsStarted = false;
		WifiState = WifiState.Off;
	}

	private void OnTimerUpdate(object? sender, EventArgs args)
	{
		var ssid = WifiSsid;
		var ssids = WifiManager.GetAvailableNetworks();

		_updateTimer?.Stop();
		if (WifiManager.IsNetworkConnected(ssid))
			WifiState = WifiState.Connected;
		else if (ssids.Contains(ssid))
		{
			WifiState = WifiState.Disconnected;
			WifiManager.ConnectToNetwork(ssid);
		}
		else
			WifiState = WifiState.Off;
		_updateTimer?.Start();

		AvailableSsids = ssids;
	}

	partial void OnWifiSsidChanged(string value)
	{
		if (!string.IsNullOrEmpty(value))
			_applicationSettings.NetworkSsid = value;
	}
	#endregion
}