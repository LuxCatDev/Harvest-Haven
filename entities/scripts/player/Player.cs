using Components.InventoryComponent;
using Components.State;
using Godot;
using GodotUtilities;
using System;

namespace Entities.Player;

[Scene]
public partial class Player : CharacterBody2D
{

	public override void _Notification(int what) {
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	public AnimationPlayer AnimationPlayer;

	[Node]
	private Node2D textures;

	[Node]
	private StateMachineComponent stateMachineComponent;

	private Vector2 direction;
	private Vector2 cardinalDirection = Vector2.Down;

	public Vector2 Direction {
		get => direction;
	}

	public string AnimationDirection
	{
		get
		{
			if (cardinalDirection == Vector2.Down)
			{
				return "down";
			}
			else if (cardinalDirection == Vector2.Up)
			{
				return "up";
			}
			else
			{
				return "side";
			}
		}
	}

	// Components
	[Node("Components/InventoryComponent")]
	public InventoryComponent InventoryComponent { get; private set; }

    public override void _Ready()
    {
		stateMachineComponent.Init();
    }

    public override void _Process(double delta)
    {
		direction = new Vector2(Input.GetAxis("left", "right"), Input.GetAxis("up", "down")).Normalized();
    }

    public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	public bool SetDirection()
	{
		if (direction == Vector2.Zero) return false;

        Vector2 newCardinalDirection = direction.Snapped(Vector2.One);

		if (newCardinalDirection == cardinalDirection) return false;

		cardinalDirection = newCardinalDirection;
		
		textures.Scale = new(cardinalDirection.X < 0 ? -1 : 1, 1);

		return true;
	}

	public void UpdateAnimation(string anim)
	{
		AnimationPlayer.Play(anim + "_" + AnimationDirection);
	}
}
