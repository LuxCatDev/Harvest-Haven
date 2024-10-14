using Common;
using Entities.Player;
using Godot;
using Objects;

namespace Components.GridComponent;

[GlobalClass]
public partial class IsCellFreeRule: ValidationRule
{
    public override bool IsValid(Object objectInstance, GridValidationComponent validationComponent)
    {
        return !objectInstance.placingArea.IsOnRestrictedCell;
    }
}