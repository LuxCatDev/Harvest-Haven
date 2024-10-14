using Godot;
using System;

namespace Components;

public enum HurtBoxType
{
	Axe,
	Pickaxe
}

public partial class HurtBoxComponent : Area2D
{
	[Export]
	public HurtBoxType Type;

	[Export]
	public int Damage;
}
