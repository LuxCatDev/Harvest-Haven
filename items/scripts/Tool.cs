using Common;
using Components.GridComponent;
using Entities.Player;
using Godot;
using GodotUtilities;

namespace Items;

[Scene]
public partial class Tool: Node2D 
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

	public void Use()
	{
		EmitSignal(SignalName.OnUsed);
		animationPlayer.Play("use_" + GameManager.Instance.Player.AnimationDirection);
	}

    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("primary_action"))
		{
			Use();
		}
    }
}