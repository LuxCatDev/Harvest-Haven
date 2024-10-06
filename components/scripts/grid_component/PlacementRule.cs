using Common;
using Godot;

namespace Components.GridComponent;

[GlobalClass]
public partial class PlacementRule: Resource
{
    public virtual bool IsValid()
    {
        return true;
    }
}