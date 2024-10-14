using System.Collections.Generic;
using Components.GridComponent;
using Godot;
using GodotUtilities;
using Models;
using Objects;

namespace Common;

[Scene]
public partial class GridValidationComponent : Node2D
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
    public Godot.Collections.Array<ValidationRule> validationRules;

    public Vector2 CrosshairSize;

    public Node2D Preview;

    public bool IsValid = true;

    public bool IsActive { get; private set; } = false;
    
    [Node]
    public Crosshair Crosshair;

    public Object ObjectInstance;

    public void Enable()
    {
        IsActive = true;
        Visible = true;
        Crosshair.Size = CrosshairSize * 16;

        Crosshair.ShowCrosshair();
        
        if (Preview != null)
        {
            Preview.Position = Crosshair.Center;

            AddChild(Preview);
        }
    
    }

    public void Disable()
    {
        IsActive = false;
        Visible = false;
        Crosshair.HideCrosshair();
        Preview?.QueueFree();
        Preview = null;
    }

    public override void _Process(double delta)
    {
        if (!IsActive) return;
        if (Crosshair == null) return;

        List<bool> validation = new();

        foreach (ValidationRule validationRule in validationRules)
        {
            validation.Add(validationRule.IsValid(ObjectInstance, this));
        }

        IsValid = validation.FindIndex((rule) => rule == false) == -1;

        Crosshair.State = IsValid;

        GlobalPosition = Crosshair.GlobalPosition;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("primary_action") && IsValid)
        {
            EmitSignal(SignalName.OnInteraction);
        }
    }
}