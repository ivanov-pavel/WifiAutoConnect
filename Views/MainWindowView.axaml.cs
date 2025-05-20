using Avalonia.Controls;

using WifiAutoConnect.ViewModels;

namespace WifiAutoConnect.Views;

public partial class MainWindowView : Window
{
	#region Constructors
	public MainWindowView()
	{
		InitializeComponent();
	}

	public MainWindowView(MainWindowModel viewModel) : this()
	{
		DataContext = viewModel;
	}
	#endregion
}