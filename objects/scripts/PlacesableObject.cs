using Components.GridComponent;
using Godot;

namespace Models;

public enum ObjectCategory
{
	Wild,
	Craftable,
	Plant,
	Common
}

[GlobalClass]
public partial class PlacesableObject: Resource 
{
	[Export]
	public PackedScene Scene { get; set; }

	[Export]
	public CompressedTexture2D Logo { get; set; }

	[Export]
	public Vector2 Size { get; set; }

	[Export]
	public ObjectCategory Category { get; set; }

	[Export]
	public Godot.Collections.Array<ValidationRule> ValidationRules;

	[Export]
	public Godot.Collections.Array<PlacementRule> PlacementRules;

	public PlacesableObject() : this(null, null, new(16, 16), new Godot.Collections.Array<ValidationRule>{},new Godot.Collections.Array<PlacementRule>{}, ObjectCategory.Common) {}

	public PlacesableObject(PackedScene scene, CompressedTexture2D logo, Vector2 size, Godot.Collections.Array<ValidationRule> validationRules,Godot.Collections.Array<PlacementRule> placementRules, ObjectCategory category)
	{
		Scene = scene;
		Logo = logo;
		Size = size;
		PlacementRules = placementRules;
		Category = category;
		ValidationRules = validationRules;
	}
}