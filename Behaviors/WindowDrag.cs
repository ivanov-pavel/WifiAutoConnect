// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;

namespace WifiAutoConnect.Behaviors;

public class WindowDrag : BehaviorBase<Control>
{
	#region Fields
	private Window? _window;
	#endregion

	#region Methods
	protected override void OnAttached()
	{
		if (AssociatedObject is not null)
		{
			_window = AssociatedObject.FindLogicalAncestorOfType<Window>();
			AssociatedObject.PointerPressed += OnPointerPressed;
		}
	}

	protected override void OnDetaching()
	{
		if (AssociatedObject is not null)
			AssociatedObject.PointerPressed -= OnPointerPressed;
	}

	private void OnPointerPressed(object? sender, PointerPressedEventArgs args)
	{
		_window?.BeginMoveDrag(args);
	}
	#endregion
}