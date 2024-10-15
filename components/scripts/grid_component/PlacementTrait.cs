using Common;
using Godot;

namespace Components.GridComponent;

[GlobalClass]
public partial class PlacementTrait: Resource
{
    public virtual bool IsValid(PlacingControllerComponent placingController)
    {
        return true;
    }

    public virtual void Execute(PlacingControllerComponent placingController)
    {

    }
}