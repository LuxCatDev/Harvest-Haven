using Godot;
using System;

namespace Items;

[GlobalClass]
public partial class ToolData : ItemData
{
	[Export]
	public PackedScene Scene;

	ToolData(): this(null) {}

	public ToolData(PackedScene scene)
	{
		Scene = scene;
	}
}
