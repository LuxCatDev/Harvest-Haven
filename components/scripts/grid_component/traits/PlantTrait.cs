using Godot;
using Objects;

namespace Components.GridComponent.Traits;

[GlobalClass]
public partial class PlantTrait : PlacementTrait
{
    public override bool IsValid(PlacingControllerComponent placingController)
    {
        return placingController.GridValidation.IsValid;
    }

    public override void Execute(PlacingControllerComponent placingController)
    {
		Plant plantInstance = placingController.PlacesableObject.Scene.Instantiate<Plant>();

		foreach (Node2D node in placingController.GridValidation.GridValidationArea.collidingWith)
		{
			if (node is Soil soil)
			{
				soil.SetPlant(plantInstance);
				return;
			}
		}
    }
}