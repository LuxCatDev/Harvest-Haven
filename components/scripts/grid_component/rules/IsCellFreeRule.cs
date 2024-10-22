using Godot;
using Objects;

namespace Components.GridComponent;

[GlobalClass]
public partial class IsCellFreeRule: ValidationRule
{
    public override bool IsValid(GridValidationAreaComponent gridValidationArea, GridValidationComponent validationComponent)
    {
        return !gridValidationArea.IsOnRestrictedCell;
    }
}