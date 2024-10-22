using Common;
using Godot;
using Objects;

namespace Components.GridComponent;

[GlobalClass]
public partial class IsNotOverPlayerRule: ValidationRule
{
    public override bool IsValid(GridValidationAreaComponent gridValidationArea, GridValidationComponent validationComponent)
    {
        return !gridValidationArea.IsOnPlayer;
    }
}