using Components.GridComponent;
using Godot;
using GodotUtilities;

namespace Common;

[Scene]
public partial class GridComponent: Node2D 
{    
    
    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes(); // this is a generated method
        }
    }

    [Export]
    private Godot.Collections.Array<PlacementRule> placementRules;

    public bool IsValid = true;

    public override void _Process(double delta)
    {
        if (GameManager.Instance.Crosshair == null) return;

        foreach(PlacementRule placementRule in placementRules)
        {
            IsValid = placementRule.IsValid();
        }

        GameManager.Instance.Crosshair.State = IsValid;
    }
}