using Godot;
using Objects;

namespace Components.GridComponent;

[GlobalClass]
public partial class IsOnSoilRule : ValidationRule
{

    public override bool IsValid(GridValidationAreaComponent gridValidationArea, GridValidationComponent validationComponent)
    {
        if (gridValidationArea.IsOnSoil)
        {
            foreach (Node2D node in gridValidationArea.collidingWith)
            {
                if (node is Soil soil && soil.Plant == null)
                {
                    return true;
                }
            }

            return false;
        }
        else
        {
            return false;
        }
    }
}