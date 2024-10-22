using Godot;
using Objects;

namespace Components.GridComponent.Traits;

[GlobalClass]
public partial class PlantTrait : Trait
{
    public override bool IsValid()
    {
    	if (!PlacingControllerComponent.IsActive) return false;
        return GridValidationComponent.IsValid;
    }

    public override void Execute()
    {
		Plant plantInstance = PlacingControllerComponent.PlacesableObject.Scene.Instantiate<Plant>();

		foreach (Node2D node in PlacingControllerComponent.GridValidation.GridValidationArea.collidingWith)
		{
			if (node is Soil soil)
			{
				soil.SetPlant(plantInstance);
				return;
			}
		}
    }
}