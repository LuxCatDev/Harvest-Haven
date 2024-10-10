using Components.InventoryComponent;
using Godot;
using GodotUtilities;
using UI;

namespace Objects;

[Scene]
public partial class ChestDialog: CanvasLayer 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private CompactInventoryDialog playerInventory;

	[Node]
	private CompactInventoryDialog chestInventory;

	public InventoryComponent Inventory;

	[Node]
	private ItemInformationPanel itemInformationPanel;

    public override void _Ready()	
    {
		playerInventory.Inventory = GameManager.Instance.Player.InventoryComponent;
		chestInventory.Inventory = Inventory;

		playerInventory.InformationPanel = itemInformationPanel;
		chestInventory.InformationPanel = itemInformationPanel;

		playerInventory.Init();

		chestInventory.Init();
    }

    public override void _Process(double delta)
    {
		if (Visible)
		{
			if (GameManager.Instance.Player.Direction != Vector2.Zero)
			{
				GameManager.Instance.DialogController.HideDialog();
			}
		}
    }
}
