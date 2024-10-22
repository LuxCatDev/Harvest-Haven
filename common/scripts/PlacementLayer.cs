using Godot;
using GodotUtilities;
using System.Collections.Generic;

namespace Common;

public partial class PlacementLayer : TileMapLayer
{
	public void SetCells(List<Vector2I> cells)
	{
		foreach(Vector2I cellCoords in cells)
		{
			SetCell(cellCoords, 0, new Vector2I(1,0));
		}
	}
}
