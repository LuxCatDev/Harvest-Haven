using Godot;

namespace Components;

public partial class HitBoxComponent : Area2D
{
	[Signal]
	public delegate void OnDamagedEventHandler();

	[Export]
	private HealthComponent healthComponent;

	[Export]
	private Godot.Collections.Array<HurtBoxType> scope;

	private void OnAreaEntered(Area2D area)
	{
		if (area is HurtBoxComponent hurtBox)
		{
			if (scope.Contains(hurtBox.Type))
			{
				healthComponent?.TakeDamage(hurtBox.Damage);
				EmitSignal(SignalName.OnDamaged);
			}
		}
	}
}
