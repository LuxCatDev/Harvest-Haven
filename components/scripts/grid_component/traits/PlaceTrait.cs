using Godot;
using Objects;

namespace Components.GridComponent.Traits;

[GlobalClass]
public partial class PlaceTrait : PlacementTrait
{
    public override bool IsValid(PlacingControllerComponent placingController)
    {
        return placingController.GridValidation.IsValid;
    }

    public override void Execute(PlacingControllerComponent placingController)
    {
		Object objectInstance = placingController.PlacesableObject.Scene.Instantiate<Object>();
		objectInstance.collisionShape.Disabled = false;
		objectInstance.GlobalPosition = placingController.GridValidation.Crosshair.CenterGlobalPosition;

		GameManager.Instance.Level.AddChild(objectInstance);
    }
}