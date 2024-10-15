using Components.GridComponent;
using Entities.Player;
using Godot;
using GodotUtilities;
using Items;

namespace Components;

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
	public PlacingControllerComponent PlacingController;

	[Export]
	public GridValidationComponent GridValidation;

	[Export]
	private Node2D itemWrapper;

	public bool Active = true;

	public InventoryItem CurrentItem = null;

	private int currentIndex;

	public bool CarryAnimation;

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

		if (PlacingController.IsActive)
		{
			PlacingController.Disable();
		}

		if (CurrentItem == null) {
			CarryAnimation = false;
			EmitSignal(SignalName.OnEquipmentChanged);
			return;
		}

		Item item = CurrentItem.Item;
		CarryAnimation = false;

		if (item.Data is ToolData toolData)
		{
			Tool tool = toolData.Scene.Instantiate<Tool>();

			tool.equipmentController = this;

			itemWrapper.AddChild(tool);

			EmitSignal(SignalName.OnToolSpawned);
		}
		else if (item.Data is Consumable consumable)
		{
			CarryAnimation = true;
		}
		else if (item.Data is Placesable placesableData)
		{
			PlacingController.ShowPreview = true;
			PlacingController.PlacesableObject = placesableData.PlacesableObject;
			PlacingController.Enable();
		}
		else if (item.Data is SeedPack seedPack)
		{
			PlacingController.ShowPreview = false;
			CarryAnimation = true;
			PlacingController.PlacesableObject = seedPack.PlacesableObject;
			PlacingController.Enable();

			Sprite2D texture = new()
			{
				Texture = CurrentItem.Item.Texture
			};

			itemWrapper.AddChild(texture);
		}
		else
		{
			CarryAnimation = true;
			Sprite2D texture = new()
			{
				Texture = CurrentItem.Item.Texture
			};

			itemWrapper.AddChild(texture);
		}

		EmitSignal(SignalName.OnEquipmentChanged);
	}
}