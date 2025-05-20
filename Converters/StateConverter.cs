// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

using WifiAutoConnect.Models;

namespace WifiAutoConnect.Converters;

public class StateConverter() : FuncValueConverter<WifiState, IImage?>(Convert)
{
	#region Properties
	private static readonly Bitmap _offIcon;
	private static readonly Bitmap _connectedIcon;
	private static readonly Bitmap _disconnectedIcon;
	#endregion

	#region Constructors
	static StateConverter()
	{
		using (var stream = AssetLoader.Open(ResourceLoader.GetUri("/Assets/WifiOff.png")))
			_offIcon = new Bitmap(stream);

		using (var stream = AssetLoader.Open(ResourceLoader.GetUri("/Assets/WifiConnected.png")))
			_connectedIcon = new Bitmap(stream);

		using (var stream = AssetLoader.Open(ResourceLoader.GetUri("/Assets/WifiDisconnected.png")))
			_disconnectedIcon = new Bitmap(stream);
	}
	#endregion

	#region Methods
	private static IImage? Convert(WifiState state)
	{
		return state switch
		{
			WifiState.Off => _offIcon,
			WifiState.Connected => _connectedIcon,
			WifiState.Disconnected => _disconnectedIcon,
			_ => null
		};
	}
	#endregion
}