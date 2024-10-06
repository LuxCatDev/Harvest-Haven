using Common;
using Godot;
using Models;

public partial class PlacingControllerComponent : Node
{
	[Export]
	public PlacesableObject Object;

	[Export]
	private GridComponent gridComponent;

    private Crosshair crosshair;

    public void Enable()
    {
		crosshair = GameManager.Instance.Crosshair;
		gridComponent.Connect(GridComponent.SignalName.OnInteraction, Callable.From(OnInteraction));
		gridComponent.Preview = Object.Preview.Instantiate<Node2D>();
		gridComponent.CrosshairSize = Object.Size;
		gridComponent.Enable();
    }

	public void Disable()
	{
		gridComponent.Disconnect(GridComponent.SignalName.OnInteraction, Callable.From(OnInteraction));
		gridComponent.Disable();
	}

	private void OnInteraction()
	{

		Node2D objectInstance = Object.Scene.Instantiate<Node2D>();

		GameManager.Instance.PlacementLayer.SetCells(crosshair.GetTargetetCells());
		objectInstance.GlobalPosition = crosshair.CenterGlobalPosition;
		GetParent<Playground>().SpawnNode(objectInstance);
	}
}
