using Godot;
using GodotUtilities;

namespace Common;

[Scene]
public partial class Playground: Node2D 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private PlacementLayer placementLayer;

	[Node]
	private Crosshair crosshair;

	[Node]
	private PlacingControllerComponent placingControllerComponent;

    public override void _Ready()
    {
		GameManager.Instance.PlacementLayer = placementLayer;
		GameManager.Instance.Crosshair = crosshair;

		placingControllerComponent.Enable();
    }

	public void SpawnNode(Node2D node)
	{
		AddChild(node);
	}
}