using Common;
using Godot;
using Models;
using Objects;

namespace Components;

public partial class PlacingControllerComponent : Node
{
	[Export]
	public PlacesableObject PlacesableObject;

	[Export]
	private GridValidationComponent gridValidation;

    private Crosshair crosshair;

	private Object objectInstance;

    public override void _Process(double delta)
    {
		if (objectInstance != null)
		{
			objectInstance.GlobalPosition = crosshair.CenterGlobalPosition;
		}
    }

    public void Enable()
    {
		objectInstance = PlacesableObject.Scene.Instantiate<Object>();
		objectInstance.collisionShape.Disabled = true;
		AddChild(objectInstance);
		crosshair =	gridValidation.Crosshair;
		gridValidation.Connect(GridValidationComponent.SignalName.OnInteraction, Callable.From(OnInteraction));
		gridValidation.CrosshairSize = PlacesableObject.Size;
		gridValidation.Enable();
    }

	public void Disable()
	{
		objectInstance.QueueFree();
		objectInstance = null;
		RemoveChild(objectInstance);
		gridValidation.Disconnect(GridValidationComponent.SignalName.OnInteraction, Callable.From(OnInteraction));
		gridValidation.Disable();
	}

	private void OnInteraction()
	{
		RemoveChild(objectInstance);
		objectInstance.collisionShape.Disabled = false;
		objectInstance.GlobalPosition = gridValidation.Crosshair.CenterGlobalPosition;

		GetParent<Playground>().SpawnNode(objectInstance);
	}
}
