using Common;
using Godot;
using Objects;

namespace Components.GridComponent;

[GlobalClass]
public partial class ValidationRule: Resource
{
    public virtual bool IsValid(Object objectInstance, GridValidationComponent validationComponent)
    {
        return true;
    }
}