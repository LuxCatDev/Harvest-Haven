using Godot;

namespace Components;

public partial class BreakableComponent : Node2D
{
	[Export]
	private HitBoxComponent hitBox;

	[Export]
	private HealthComponent healthComponent;

	[Export]
	private DropperComponent dropperComponent;

	[Export]
	private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
		healthComponent.OnHpChanged += OnTakeDamage;
		healthComponent.OnDeath += OnDeath;
		animationPlayer.AnimationFinished += OnAnimationFinished;
    }

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
		if (dropperComponent != null)
		{
			dropperComponent.OnDropFinished += () => GetParent().QueueFree();
		}

		if (anim == "death")
		{
			dropperComponent?.Drop();
		}
	}
}
