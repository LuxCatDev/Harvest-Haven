using Common;
using Godot;

namespace Components.GridComponent;

[GlobalClass]
public partial class IsCloseToPlayerRule: PlacementRule
{

	[Export]
	private float expectedDistance = 16;

    public override bool IsValid()
    {
        Crosshair crosshair = GameManager.Instance.Crosshair;
        return crosshair.DistanceToPlayer < expectedDistance;
    }
}