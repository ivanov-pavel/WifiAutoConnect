// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;

namespace WifiAutoConnect.Models;

public static class ResourceLoader
{
	#region Constants
	private const string ResourcesPath = "avares://WifiAutoConnect";
	#endregion

	#region Methods
	public static Uri GetUri(string path) => new($"{ResourcesPath}{path}");
	#endregion
}