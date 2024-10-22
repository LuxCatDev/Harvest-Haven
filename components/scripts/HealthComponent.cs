using Godot;
using System;

namespace Components;

public partial class HealthComponent : Node
{
	[Signal]
	public delegate void OnHpChangedEventHandler(int hp);

	[Signal]
	public delegate void OnDeathEventHandler();

	[Export]
	private int max_hp;

	public int Hp;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hp = max_hp;
	}

	public void TakeDamage(int damage)
	{
		Hp -= damage;

		if (Hp <= 0)
		{
			Hp = 0;
			EmitSignal(SignalName.OnDeath);
		} else {
			EmitSignal(SignalName.OnHpChanged, Hp);
		}
	}

	public void Heal(int healing)
	{
		if (Hp + healing > max_hp)
		{
			Hp = max_hp;
		} else {
			Hp += healing;
		}

		EmitSignal(SignalName.OnHpChanged, Hp);
	}
}
