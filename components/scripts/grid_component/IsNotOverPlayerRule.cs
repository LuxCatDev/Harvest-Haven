using Common;
using Entities.Player;
using Godot;

namespace Components.GridComponent;

[GlobalClass]
public partial class IsNotOverPlayerRule: PlacementRule
{
    public override bool IsValid()
    {
        return !GameManager.Instance.Crosshair.IsOnPlayer;
    }
}