// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Collections.ObjectModel;

using Avalonia;

namespace WifiAutoConnect.Behaviors;

public class BehaviorsCollection : Collection<BehaviorBase>
{
	#region Fields
	private readonly object _associatedObject;
	#endregion

	#region Properties
	public static readonly AttachedProperty<BehaviorsCollection?> ItemsProperty =
		AvaloniaProperty.RegisterAttached<BehaviorsCollection, AvaloniaObject, BehaviorsCollection?>("Items");
	#endregion

	#region Constructors
	private BehaviorsCollection(object associatedObject)
	{
		_associatedObject = associatedObject;
	}
	#endregion

	#region Methods
	private void AttachBehaviors()
	{
		foreach (var behavior in Items)
		{
			behavior.Detach();
			behavior.Attach(_associatedObject);
		}
	}

	private void DetachBehaviors()
	{
		foreach (var behavior in Items)
			behavior.Detach();
	}

	public static BehaviorsCollection GetItems(AvaloniaObject avaloniaObject)
	{
		var behaviors = avaloniaObject.GetValue(ItemsProperty);
		if (behaviors is not null)
			return behaviors;

		behaviors = new BehaviorsCollection(avaloniaObject);
		avaloniaObject.SetValue(ItemsProperty, behaviors);

		if (avaloniaObject is Visual visual)
		{
			visual.AttachedToVisualTree -= OnAttachedToVisualTree;
			visual.AttachedToVisualTree += OnAttachedToVisualTree;
			visual.DetachedFromVisualTree -= OnDetachedFromVisualTree;
			visual.DetachedFromVisualTree += OnDetachedFromVisualTree;
		}

		return behaviors;
	}

	private static void OnDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs args)
	{
		(sender as AvaloniaObject)?.GetValue(ItemsProperty)?.DetachBehaviors();
	}

	private static void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs args)
	{
		(sender as AvaloniaObject)?.GetValue(ItemsProperty)?.AttachBehaviors();
	}
	#endregion
}