using Entities.Player;
using Godot;
using GodotUtilities;

namespace Components;

[Scene]
public partial class InteractionComponent: Area2D 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Signal]
	public delegate void OnInteractionEventHandler();

	public bool CanInteract;

    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("interact"))
		{
			if (CanInteract)
			{
				EmitSignal(SignalName.OnInteraction);
			}
		}
    }

    private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			CanInteract = true;
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Player player)
		{
			CanInteract = false;
		}
	}
}