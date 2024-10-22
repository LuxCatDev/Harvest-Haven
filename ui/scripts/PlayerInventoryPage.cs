using System.Collections.Generic;
using System.Linq;
using Components;
using Godot;
using GodotUtilities;

namespace UI;

[Scene]
public partial class PlayerInventoryPage: Control 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node("Content/GridContainer")]
	private GridContainer gridContainer;

	[Node]
	private ItemInformationPanel itemInformationPanel;

	private List<InventorySlot> slots;

	private InventoryComponent inventory;

	private PackedScene slotScene;

	public override void _Ready()
	{
		slots = new();

		slotScene = GD.Load<PackedScene>("res://ui/scenes/inventory_slot.tscn");
		inventory = GameManager.Instance.Player.InventoryComponent;

		inventory.Updated += OnInventoryUpdated;

		foreach(int index in Enumerable.Range(0, inventory.Size))
		{
			InventorySlot slot = slotScene.Instantiate<InventorySlot>();
			slot.SetIndex(index);
			if (inventory.items[slot.Index] != null)
			{
				slot.SetItem(inventory.items[slot.Index]);
			}
			slot.Inventory = inventory;
			slot.InformationPanel = itemInformationPanel;

			gridContainer.AddChild(slot);

			slots.Add(slot);
		}
	}

	public void OnInventoryUpdated()
	{
		foreach(InventorySlot slot in slots)
		{
			if (inventory.items[slot.Index] != null)
			{
				slot.SetItem(inventory.items[slot.Index]);
			} else {
				slot.SetEmpty();
			}
		}
	}
}
