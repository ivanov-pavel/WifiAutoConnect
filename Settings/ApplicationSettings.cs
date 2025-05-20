// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WifiAutoConnect.Settings;

public class ApplicationSettings
{
	#region Constants
	private const string SettingsFile = "settings.json";
	#endregion

	#region Properties
	public string NetworkSsid { get; set; }
	public int UpdateInterval { get; set; }

	public static ApplicationSettings DefaultSettings => new()
	{
		NetworkSsid = string.Empty,
		UpdateInterval = 1
	};
	#endregion

	#region Constructors
	public ApplicationSettings()
	{
		NetworkSsid = string.Empty;
		UpdateInterval = 1;
	}
	#endregion

	#region Methods
	public static ApplicationSettings? LoadSettings()
	{
		if (!File.Exists(SettingsFile))
			return null;

		try
		{
			using var stream = File.Open(SettingsFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			return JsonSerializer.Deserialize<ApplicationSettings>(stream, ApplicationSettingsContext.Default.ApplicationSettings);
		}
		catch (Exception exception)
		{
			Debug.WriteLine(exception);
			return null;
		}
	}

	public bool SaveSettings()
	{
		try
		{
			using var stream = File.Open(SettingsFile, FileMode.Create, FileAccess.Write, FileShare.Read);
			JsonSerializer.Serialize(stream, this, ApplicationSettingsContext.Default.ApplicationSettings);
			return true;
		}
		catch (Exception exception)
		{
			Debug.WriteLine(exception);
			return false;
		}
	}
	#endregion
}

[JsonSerializable(typeof(ApplicationSettings))]
[JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class ApplicationSettingsContext : JsonSerializerContext;