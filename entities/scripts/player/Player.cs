using Components;
using Components.State;
using Godot;
using GodotUtilities;
using Items;

namespace Entities.Player;

[Scene]
public partial class Player : CharacterBody2D
{

	public override void _Notification(int what)
	{
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
	public Vector2 CardinalDirection
	{
		get;
		private set;
	}

	public Vector2 Direction
	{
		get => direction;
	}

	public string AnimationDirection
	{
		get
		{
			if (CardinalDirection == Vector2.Down)
			{
				return "down";
			}
			else if (CardinalDirection == Vector2.Up)
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

	[Node("Components/EquipmentControllerComponent")]
	public EquipmentControllerComponent equipmentControllerComponent;

	public override void _Ready()
	{
		CardinalDirection = Vector2.Down;
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

		if (newCardinalDirection == CardinalDirection) return false;

		CardinalDirection = newCardinalDirection;

		textures.Scale = new(CardinalDirection.X < 0 ? -1 : 1, 1);

		return true;
	}

	public void UpdateAnimation(string anim)
	{
		if (equipmentControllerComponent.CarryAnimation && (anim == "move" || anim == "idle"))
		{
			AnimationPlayer.Play(anim + "_carry_" + AnimationDirection);
		}
		else
		{
			AnimationPlayer.Play(anim + "_" + AnimationDirection);
		}
	}
}
