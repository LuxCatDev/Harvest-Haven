using Entities.Player;
using Godot;
using GodotUtilities;

namespace Components;

[Scene]
public partial class PlacingAreaComponent: Area2D 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private CollisionShape2D collisionShape;

	[Export]
	private Vector2 size = new(1,1);

	public bool IsOnPlayer = false;

	public bool IsOnRestrictedCell = false;

	public override void _Ready()
	{
		collisionShape.Shape.Set("size", size * 16);
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			IsOnPlayer = true;

			return;
		}

		IsOnRestrictedCell = true;
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Player)
		{
			IsOnPlayer = false;

			return;
		}

		IsOnRestrictedCell = false;
	}
}