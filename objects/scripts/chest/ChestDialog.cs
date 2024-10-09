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

    public override void _Ready()	
    {
		playerInventory.Inventory = GameManager.Instance.Player.InventoryComponent;
		chestInventory.Inventory = Inventory;

		playerInventory.Init();

		chestInventory.Init();
    }

	public void ShowDialog()
	{
		GetTree().Paused = true;
		Visible = true;
	}

    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("exit"))
		{
			GetTree().Paused = false;
			Visible = false;
		}
    }
}
