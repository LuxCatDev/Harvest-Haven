using Entities.Player;
using Godot;
using GodotUtilities;
using Items;

namespace Common;

[Scene]
public partial class PickableItem: Node2D 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private Sprite2D texture;

	[Export]
	private InventoryItem inventoryItem;

    public override void _Ready()
    {
		texture.Texture = inventoryItem.Item.Texture;
    }

	public void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.InventoryComponent.AddItem(inventoryItem);
			QueueFree();
		}
	}
}