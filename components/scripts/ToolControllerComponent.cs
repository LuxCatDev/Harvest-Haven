using System.Collections.Generic;
using Common;
using Components.GridComponent;
using Godot;
using GodotUtilities;
using Items;

namespace Components;

[Scene]
public partial class ToolControllerComponent: Node2D 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	public HurtBoxComponent HurtBox;

	public ToolData toolData;

	[Export]
	public GridValidationComponent GridValidation;

	private Crosshair crosshair;

	public bool IsActive;

    public void Enable()
	{
		IsActive = true;
		crosshair = GridValidation.Crosshair;
		crosshair.UsingTool = true;
		GridValidation.CrosshairSize = new (1,1);
		GridValidation.ValidationRules = toolData.ValidationRules;
		GridValidation.Enable();
		GridValidation.Visible = false;
	}

	public void Disable()
	{
		IsActive = false;
		crosshair.UsingTool = false;
		GridValidation.Disable();
	}

    public override void _Process(double delta)
    {
		if (IsActive)
		{
			HurtBox.GlobalPosition = crosshair.CenterGlobalPosition;
		}
    }

    public void Interact()
	{
		if (!IsActive) return;

		List<bool> validations = new();

		foreach(Trait trait in toolData.Traits)
		{
			trait.Init();
			validations.Add(trait.IsValid());
		}

		if (validations.FindIndex((item) => item == false) != -1)
		{
			return;
		}

		foreach(Trait trait in toolData.Traits)
		{
			trait.Execute();
		}
	}
}