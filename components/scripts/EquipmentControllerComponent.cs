using Common;
using Components;
using Components.InventoryComponent;
using Entities.Player;
using Godot;
using GodotUtilities;
using Items;

namespace Namespace;

[Scene]
public partial class EquipmentControllerComponent : Node2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Signal]
	public delegate void OnEquipmentChangedEventHandler();

	[Signal]
	public delegate void OnToolSpawnedEventHandler();

	[Export]
	private InventoryComponent inventoryComponent;

	[Export]
	private PlacingControllerComponent placingController;

	[Export]
	private Node2D itemWrapper;

	public bool Active = true;

	public InventoryItem CurrentItem = null;

	private int currentIndex;

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("1"))
		{
			EquipItem(0);
		}

		if (@event.IsActionPressed("2"))
		{
			EquipItem(1);
		}

		if (@event.IsActionPressed("3"))
		{
			EquipItem(2);
		}

		if (@event.IsActionPressed("4"))
		{
			EquipItem(3);
		}

		if (@event.IsActionPressed("5"))
		{
			EquipItem(4);
		}

		if (@event.IsActionPressed("6"))
		{
			EquipItem(5);
		}

		if (@event.IsActionPressed("7"))
		{
			EquipItem(6);
		}

		if (@event.IsActionPressed("8"))
		{
			EquipItem(7);
		}

		if (@event.IsActionPressed("next_item"))
		{
			if (currentIndex >= 7)
			{
				EquipItem(0);
			}
			else
			{
				EquipItem(currentIndex + 1);
			}
		}

		if (@event.IsActionPressed("prev_item"))
		{
			if (currentIndex <= 0)
			{
				EquipItem(7);
			}
			else
			{
				EquipItem(currentIndex - 1);
			}
		}
	}

	public void EquipItem(int index)
	{
		if (!Active) return;

		currentIndex = index;

		CurrentItem = inventoryComponent.items[index];

		EmitSignal(SignalName.OnEquipmentChanged);
		
		if (itemWrapper.GetChildCount() > 0)
		{
			itemWrapper.RemoveChild(itemWrapper.GetChild(0));
		}

		placingController.Disable();

		if (CurrentItem == null) return;

		if (CurrentItem.Item.Type == ItemType.Tool && CurrentItem.Item.Data is ToolData toolData)
		{
			Tool tool = toolData.Scene.Instantiate<Tool>();

			itemWrapper.AddChild(tool);

			EmitSignal(SignalName.OnToolSpawned);
		}
		else
		{
			Sprite2D texture = new() {
				Texture = CurrentItem.Item.Texture
			};

			itemWrapper.AddChild(texture);
		}
	}
}