using Godot;
using GodotUtilities;
using System.Collections.Generic;

namespace Common;

[Scene]
public partial class PlacementLayer : Node2D
{

	 public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes(); // this is a generated method
        }
    }

	[Node]
	public TileMapLayer TileMapLayer;

	public void SetCells(List<Vector2I> cells)
	{
		foreach(Vector2I cellCoords in cells)
		{
			TileMapLayer.SetCell(cellCoords, 0, new Vector2I(1,0));
		}
	}
}
