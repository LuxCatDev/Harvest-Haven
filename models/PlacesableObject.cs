using Components.GridComponent;
using Godot;

namespace Models;

[GlobalClass]
public partial class PlacesableObject: Resource 
{
	[Export]
	public PackedScene Scene { get; set; }

	[Export]
	public PackedScene Preview { get; set; }

	[Export]
	public AtlasTexture Logo { get; set; }

	[Export]
	public Vector2 Size { get; set; }

	[Export]
	public Godot.Collections.Array<PlacementRule> PlacementRules;

	public PlacesableObject() : this(null, null, null, new(16, 16), new Godot.Collections.Array<PlacementRule>{}) {}

	public PlacesableObject(PackedScene scene, PackedScene preview, AtlasTexture logo, Vector2 size, Godot.Collections.Array<PlacementRule> placementRules)
	{
		Scene = scene;
		Preview = preview;
		Logo = logo;
		Size = size;
		PlacementRules = placementRules;
	}
}