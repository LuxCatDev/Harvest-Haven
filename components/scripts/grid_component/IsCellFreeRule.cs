using Common;
using Entities.Player;
using Godot;

namespace Components.GridComponent;

[GlobalClass]
public partial class IsCellFreeRule: PlacementRule
{
    public override bool IsValid()
    {
        return !GameManager.Instance.Crosshair.IsOnRestrictedCell;
    }
}