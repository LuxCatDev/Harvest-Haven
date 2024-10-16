using Entities.Player;
using Godot;
using GodotUtilities;

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

	[Node]
	public AnimationPlayer animationPlayer;

	[Export]
	public InventoryItem InventoryItem;

	public Vector2 Direction = Vector2.Zero;

	public Vector2 Velocity = Vector2.Zero;

	private bool ready;

	private bool toPlayer;

	public override void _Ready()
	{
		texture.Texture = InventoryItem.Item.Texture;
		GetTree().CreateTimer(0.5).Timeout += () =>
		{
			ready = true;
			pickingArea.Monitoring = true;
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		KinematicCollision2D collision2D = MoveAndCollide(Velocity * (float)delta);

		if (collision2D != null)
		{
			Velocity = Velocity.Bounce(collision2D.GetNormal());
		}

		Velocity -= Velocity * (float)delta * 4;
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