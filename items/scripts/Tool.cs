using Components;
using Entities.Player;
using Godot;
using GodotUtilities;

namespace Items;

[Scene]
public partial class Tool : Node2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Signal]
	public delegate void OnUsedEventHandler();

	[Export]
	private AnimationPlayer animationPlayer;

	[Export]
	public ToolControllerComponent ToolControllerComponent;

	public Vector2 CardinalDirection { get; private set; }

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

	public void Use()
	{
		Player player = GameManager.Instance.Player;

		CardinalDirection = player.GlobalPosition.DirectionTo(GetGlobalMousePosition()).Snapped(Vector2.One);
		EmitSignal(SignalName.OnUsed);
		animationPlayer.Play("use_" + AnimationDirection);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("primary_action"))
		{
			Use();
		}
	}

	public void Contact()
	{
		ToolControllerComponent.Interact();
	}
}