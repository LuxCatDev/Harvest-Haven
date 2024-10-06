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

    public override void _Ready()
    {
		GameManager.Instance.PlacementLayer = placementLayer;
		GameManager.Instance.Crosshair = crosshair;

		crosshair.Size = new(16, 16);

		crosshair.ShowCrosshair();
    }
}