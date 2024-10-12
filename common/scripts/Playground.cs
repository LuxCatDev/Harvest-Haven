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
	private Player player;

	[Node]
	private DialogController dialogController;

    public override void _Ready()
    {
		GameManager.Instance.Player = player;
		GameManager.Instance.Level = this;
		GameManager.Instance.DialogController = dialogController;
		GameManager.Instance.PlacementLayer = placementLayer;
    }

	public void SpawnNode(Node2D node)
	{
		AddChild(node);
	}
}