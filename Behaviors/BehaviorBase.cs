// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Avalonia;

namespace WifiAutoConnect.Behaviors;

public abstract class BehaviorBase : AvaloniaObject
{
	#region Fields
	protected object? _associatedObject;
	#endregion

	#region Methods
	public void Attach(object? associatedObject)
	{
		if (Equals(_associatedObject, associatedObject))
			return;

		_associatedObject = associatedObject;
		OnAttached();
	}

	public void Detach()
	{
		OnDetaching();
		_associatedObject = null;
	}

	protected virtual void OnAttached()
	{
	}

	protected virtual void OnDetaching()
	{
	}
	#endregion
}

public abstract class BehaviorBase<T> : BehaviorBase where T : class
{
	#region Properties
	protected T? AssociatedObject => _associatedObject as T;
	#endregion
}