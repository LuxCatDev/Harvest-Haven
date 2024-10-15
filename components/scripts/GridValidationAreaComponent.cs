using System.Collections.Generic;
using Entities.Player;
using Godot;
using GodotUtilities;
using Objects;

namespace Components;

[Scene]
public partial class GridValidationAreaComponent: Area2D 
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
	public Vector2 Size = new(1, 1);

	public bool IsOnPlayer {
		get {
			bool res = false;

	 		foreach(Node2D node in collidingWith)
			{
				if (node is Player)
				{
					res = true;
				}
			}

			return res;
		}
	}

	public bool IsOnSoil {
		get {
			bool res = false;

	 		foreach(Node2D node in collidingWith)
			{
				if (node is Soil)
				{
					res = true;
				}
			}

			return res;
		}
	}

	public bool IsOnRestrictedCell
	{
		get
		{
			bool res = false;

			foreach(Node2D node in collidingWith)
			{
				if (node is Soil)
				{
					res = true;
				} else if (node is Player)
				{
					
				} else {
					res = true;
				}
			}

			return res;
		}
	}

	public int areasColliding = 0;

	public List<Node2D> collidingWith;

	public void Enable()
	{
		collidingWith = new();
		Monitoring = true;
		Size = new(Size.X * 16 - 1, Size.Y * 16 - 1);
		collisionShape2D.Shape = new RectangleShape2D()
		{
			Size = Size
		};
	}

	public void Disable()
	{
		Monitoring = false;
	}

	private void OnAreaEntered(Area2D area)
	{
		collidingWith.Add(area);
	}

	private void OnAreaExited(Area2D area)
	{
		int index = collidingWith.FindIndex((node) => node == area);

		if (index != -1)
		{
			collidingWith.RemoveAt(index);
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		collidingWith.Add(body);
	}

	private void OnBodyExited(Node2D body)
	{
		int index = collidingWith.FindIndex((node) => node == body);

		if (index != -1)
		{
			collidingWith.RemoveAt(index);
		}
	}
}