using System.Collections.Generic;
using System.Drawing;
using Components.GridComponent;
using Entities.Player;
using Godot;
using GodotUtilities;
using Objects;

namespace Components;

[Scene]
public partial class PlacingAreaComponent : Area2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private CollisionShape2D collisionShape2D;

	[Export]
	private Vector2 size = new(1, 1);

	public override void _Ready()
	{
		size = new(size.X * 16 - 1, size.Y * 16 - 1);
		collisionShape2D.Shape = new RectangleShape2D()
		{
			Size = size
		};
	}
}