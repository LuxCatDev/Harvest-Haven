using Entities.Player;
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

	[Node]
	private Player player;

    public override void _Ready()
    {
		GameManager.Instance.PlacementLayer = placementLayer;
		GameManager.Instance.Crosshair = crosshair;
		GameManager.Instance.Player = player;

		placingControllerComponent.Enable();

		// PackedScene UIScene = GD.Load<PackedScene>("res://ui/scenes/UI.tscn");

		// AddChild(UIScene.Instantiate());
    }

	public void SpawnNode(Node2D node)
	{
		AddChild(node);
	}
}