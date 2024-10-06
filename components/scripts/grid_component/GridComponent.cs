using System.Collections.Generic;
using Components.GridComponent;
using Godot;
using GodotUtilities;

namespace Common;

[Scene]
public partial class GridComponent : Node2D
{

    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes(); // this is a generated method
        }
    }

    [Signal]
    public delegate void OnInteractionEventHandler();

    [Export]
    private Godot.Collections.Array<PlacementRule> placementRules;

    public Vector2 CrosshairSize;

    public Node2D Preview;

    public bool IsValid = true;

    public bool IsActive { get; private set; } = false;

    private Crosshair crosshair;

    public void Enable()
    {
        IsActive = true;
        Visible = true;
        crosshair = GameManager.Instance.Crosshair;
        crosshair.Size = CrosshairSize * 16;

        crosshair.ShowCrosshair();
        
        Preview.Position = crosshair.Center;

        AddChild(Preview);
    }

    public void Disable()
    {
        IsActive = false;
        Visible = false;
        crosshair.HideCrosshair();
        Preview.QueueFree();
        Preview = null;
    }

    public override void _Process(double delta)
    {
        if (!IsActive) return;
        if (crosshair == null) return;

        List<bool> validation = new();

        foreach (PlacementRule placementRule in placementRules)
        {
            validation.Add(placementRule.IsValid());
        }

        IsValid = validation.FindIndex((rule) => rule == false) == -1;

        GameManager.Instance.Crosshair.State = IsValid;

        GlobalPosition = GameManager.Instance.Crosshair.GlobalPosition;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && IsValid)
        {
            EmitSignal(SignalName.OnInteraction);
        }
    }
}