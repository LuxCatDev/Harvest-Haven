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
            WireNodes();
        }
    }

    [Signal]
    public delegate void OnInteractionEventHandler();

    [Export]
    public Godot.Collections.Array<PlacementRule> placementRules;

    public Vector2 CrosshairSize;

    public Node2D Preview;

    public bool IsValid = true;

    public bool IsActive { get; private set; } = false;
    
    [Node]
    private Crosshair crosshair;

    public override void _Ready()
    {
        GameManager.Instance.Crosshair = crosshair;
    }

    public void Enable()
    {
        IsActive = true;
        Visible = true;
        crosshair.Size = CrosshairSize * 16;

        crosshair.ShowCrosshair();
        
        if (Preview != null)
        {
            Preview.Position = crosshair.Center;

            AddChild(Preview);
        }
    
    }

    public void Disable()
    {
        IsActive = false;
        Visible = false;
        crosshair.HideCrosshair();
        Preview?.QueueFree();
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
        if (@event.IsActionPressed("primary_action") && IsValid)
        {
            EmitSignal(SignalName.OnInteraction);
        }
    }
}