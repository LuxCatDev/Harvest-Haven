using System.Collections.Generic;
using System.Linq;
using Components.InventoryComponent;
using Godot;
using GodotUtilities;

namespace UI;

[Scene]
public partial class CompactInventoryDialog : Panel
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node("ScrollContainer/GridContainer")]
	private GridContainer gridContainer;

	[Node]
	private Label title;

	[Export]
	private string name;

	private List<InventorySlot> slots;

	public InventoryComponent Inventory;

	private PackedScene slotScene;

	public ItemInformationPanel InformationPanel;

	public void Init()
	{
		slots = new();

		slotScene = GD.Load<PackedScene>("res://ui/scenes/inventory_slot.tscn");

		Inventory.Updated += OnInventoryUpdated;

		title.Text = name;

		foreach (int index in Enumerable.Range(0, Inventory.Size))
		{
			InventorySlot slot = slotScene.Instantiate<InventorySlot>();
			slot.Index = index;
			if (Inventory.items[slot.Index] != null)
			{
				slot.SetItem(Inventory.items[slot.Index]);
			}
			slot.Inventory = Inventory;
			slot.InformationPanel = InformationPanel;

			gridContainer.AddChild(slot);

			slots.Add(slot);
		}
	}

	public void OnInventoryUpdated()
	{
		foreach (InventorySlot slot in slots)
		{
			if (Inventory.items[slot.Index] != null)
			{
				slot.SetItem(Inventory.items[slot.Index]);
			}
			else
			{
				slot.SetEmpty();
			}
		}
	}
}