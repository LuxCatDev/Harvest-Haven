using System.Collections.Generic;
using Common;
using Godot;
using GodotUtilities;
using Models;

namespace Components.GridComponent;

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

    private Godot.Collections.Array<ValidationRule> validationRules;

    public Vector2 CrosshairSize;

    public bool IsValid = true;

    public bool IsActive { get; private set; } = false;
    
    [Node]
    public Crosshair Crosshair;

    [Node("GridValidationAreaComponent")]
    public GridValidationAreaComponent GridValidationArea;

    public PlacesableObject PlacesableObject;

    public void Enable()
    {
        validationRules = PlacesableObject.ValidationRules;
        IsActive = true;
        Visible = true;
        Crosshair.Size = CrosshairSize * 16;
        GridValidationArea.Size = CrosshairSize;
        GridValidationArea.Enable();

        Crosshair.ShowCrosshair(); 
    }

    public void Disable()
    {
        IsActive = false;
        Visible = false;
        GridValidationArea.Disable();
        Crosshair.HideCrosshair();
    }

    public override void _Process(double delta)
    {
        if (!IsActive) return;
        if (Crosshair == null) return;

        List<bool> validation = new();

        foreach (ValidationRule validationRule in validationRules)
        {
            validation.Add(validationRule.IsValid(GridValidationArea, this));
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