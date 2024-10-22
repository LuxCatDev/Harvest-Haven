using Components.GridComponent;
using Godot;
using System;

namespace Items;

public enum ToolType {
	Common,
	Axe,
	WateringCan
}

[GlobalClass]
public partial class ToolData : ItemData
{
	[Export]
	public PackedScene Scene;

	[Export]
	public ToolType Type;

	[Export]
	public Godot.Collections.Array<ValidationRule> ValidationRules;

	[Export]
	public Godot.Collections.Array<Trait> Traits;

	ToolData(): this(null, new Godot.Collections.Array<ValidationRule>{}, new Godot.Collections.Array<Trait>{}, ToolType.Common) {}

	public ToolData(PackedScene scene, Godot.Collections.Array<ValidationRule> validationRules, Godot.Collections.Array<Trait> traits, ToolType type)
	{
		Scene = scene;
		ValidationRules = validationRules;
		Traits = traits;
		Type = type;
	}
}
