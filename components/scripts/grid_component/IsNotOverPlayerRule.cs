using Common;
using Godot;
using Objects;

namespace Components.GridComponent;

[GlobalClass]
public partial class IsNotOverPlayerRule: ValidationRule
{
    public override bool IsValid(Object objectInstance, GridValidationComponent validationComponent)
    {
        return !objectInstance.placingArea.IsOnPlayer;
    }
}