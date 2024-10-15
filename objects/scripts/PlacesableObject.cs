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
	public Vector2 Size { get; set; }

	[Export]
	public ObjectCategory Category { get; set; }

	[Export]
	public Godot.Collections.Array<ValidationRule> ValidationRules;

	[Export]
	public Godot.Collections.Array<PlacementTrait> PlacementTraits;

	public PlacesableObject() : this(null, new(16, 16), new Godot.Collections.Array<ValidationRule>{}, new Godot.Collections.Array<PlacementTrait>{}, ObjectCategory.Common) {}

	public PlacesableObject(PackedScene scene, Vector2 size, Godot.Collections.Array<ValidationRule> validationRules, Godot.Collections.Array<PlacementTrait> placementTraits, ObjectCategory category)
	{
		Scene = scene;
		Size = size;
		Category = category;
		ValidationRules = validationRules;
		PlacementTraits = placementTraits;
	}
}