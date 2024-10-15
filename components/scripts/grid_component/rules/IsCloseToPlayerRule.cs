using Common;
using Godot;
using Objects;

namespace Components.GridComponent;

[GlobalClass]
public partial class IsCloseToPlayerRule: ValidationRule
{

	[Export]
	private float expectedDistance = 16;

    public override bool IsValid(GridValidationAreaComponent gridValidationArea, GridValidationComponent validationComponent)
    {
        Crosshair crosshair = validationComponent.Crosshair;
        return crosshair.DistanceToPlayer < expectedDistance;
    }
}