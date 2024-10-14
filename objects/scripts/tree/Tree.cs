using Components;
using Godot;
using GodotUtilities;

namespace Objects;

[Scene]
public partial class Tree: Object 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private HealthComponent healthComponent;
	
	[Node]
	private AnimationPlayer animationPlayer;

	[Node]
	private DropperComponent dropperComponent;

	private void OnTakeDamage(int hp)
	{
		animationPlayer.Play("hited");
	}

	private void OnDeath()
	{
		animationPlayer.Play("death");
	}

	private void OnAnimationFinished(Godot.StringName anim)
	{
		dropperComponent.OnDropFinished += () => QueueFree();

		if (anim == "death")
		{
			dropperComponent.Drop();
		}
	}
}
