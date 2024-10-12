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

	ToolData(): this(null, ToolType.Common) {}

	public ToolData(PackedScene scene, ToolType type)
	{
		Scene = scene;
		Type = type;
	}
}
