using Entities.Player;
using Godot;
using GodotUtilities;
using Items;

namespace Common;

[Scene]
public partial class PickableItem : StaticBody2D
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

	[Node]
	private Area2D pickingArea;

	[Export]
	public InventoryItem InventoryItem;

	public Vector2 velocity = Vector2.Zero;

	private bool ready;

	private bool toPlayer;

	public override void _Ready()
	{
		texture.Texture = InventoryItem.Item.Texture;
		GetTree().CreateTimer(0.5).Timeout += () => {
			ready = true;
			pickingArea.Monitoring = true;
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		if (toPlayer && ready)
		{
			velocity = GlobalPosition.DirectionTo(GameManager.Instance.Player.GlobalPosition) * 75;
		}
		if (ready)
		{
			MoveAndCollide(velocity * (float)delta);
		} else {
			velocity -= velocity * (float)delta;
			MoveAndCollide(velocity * (float)delta);
		}
	}

	public void OnDetectionAreaBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			toPlayer = true;
		}
	}

	public void OnPickingAreaBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			player.InventoryComponent.AddItem(InventoryItem);
			QueueFree();
		}
	}
}