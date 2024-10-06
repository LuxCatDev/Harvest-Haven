using Godot;
using GodotUtilities;

namespace Objects;

[GlobalClass]
public partial class Object: Resource 
{
	[Export]
	public PackedScene scene;
}