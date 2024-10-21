using System.Collections.Generic;
using Common;
using Components.GridComponent;
using Godot;
using Models;
using Objects;

namespace Components;

public partial class PlacingControllerComponent : Node2D
{
	[Export]
	public PlacesableObject PlacesableObject;

	[Export]
	public GridValidationComponent GridValidation;

	[Export]
	public InventoryComponent InventoryComponent;

	private Crosshair crosshair;

	public Object Preview { get; private set; }

	public bool ShowPreview;

	public bool IsActive;

	public override void _Process(double delta)
	{
		if (Preview != null)
		{
			Preview.GlobalPosition = crosshair.CenterGlobalPosition;
		}
	}

    public override void _Ready()
    {
		GridValidation.OnInteraction += OnInteraction;
    }

    public void Enable()
	{
		IsActive = true;
		if (ShowPreview)
		{
			Preview = PlacesableObject.Scene.Instantiate<Object>();
			Preview.collisionShape.Disabled = true;
			AddChild(Preview);
		}

		crosshair = GridValidation.Crosshair;
		GridValidation.CrosshairSize = PlacesableObject.Size;
		GridValidation.Enable();
	}

	public void Disable()
	{
		IsActive = false;
		if (Preview != null)
		{
			Preview.QueueFree();
			Preview = null;
		}
		GridValidation.Disable();
	}

	public void OnInteraction()
	{
		if (!IsActive) return;

		List<bool> validations = new();

		foreach(Trait trait in PlacesableObject.Traits)
		{
			trait.Init();
			validations.Add(trait.IsValid());
		}

		if (validations.FindIndex((item) => item == false) != -1)
		{
			return;
		}

		foreach(Trait trait in PlacesableObject.Traits)
		{
			trait.Execute();
		}
	}
}
