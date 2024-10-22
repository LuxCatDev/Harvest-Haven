using Common;
using Godot;
using Objects;

namespace Components.GridComponent;

[GlobalClass]
public partial class ValidationRule: Resource
{
    public virtual bool IsValid(GridValidationAreaComponent gridValidationArea, GridValidationComponent validationComponent)
    {
        return true;
    }
}