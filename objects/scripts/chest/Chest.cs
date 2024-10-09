using Components.InventoryComponent;
using Godot;
using GodotUtilities;

namespace Objects;

[Scene]
public partial class Chest : StaticBody2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private InventoryComponent inventoryComponent;

	private PackedScene chestDialogScene = GD.Load<PackedScene>("res://objects/scenes/chest/chest_dialog.tscn");

	private ChestDialog chestDialog;

	private void OnInteraction()
	{
		if (chestDialog != null && chestDialog.Visible) return;

		if (chestDialog == null)
		{
			chestDialog = chestDialogScene.Instantiate<ChestDialog>();

			chestDialog.Inventory = inventoryComponent;

			AddChild(chestDialog);
		}

		GameManager.Instance.DialogController.ShowDialog(chestDialog);
	}
}

