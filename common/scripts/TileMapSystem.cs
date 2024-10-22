using Godot;
using GodotUtilities;
using System;
using System.Collections.Generic;

namespace Common;

using static TileType;

public enum TileType
{
	None,
	Grass,
	Hole,
	Water,
}

[Scene]
public partial class TileMapSystem : Node2D
{

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private TileMapLayer displayLayer;

	[Export]
	private TileMapLayer dataLayer;

	readonly Vector2I[] NEIGHTBOURS = new Vector2I[] { new(0, 0), new(1, 0), new(0, 1), new(1, 1) };

	readonly Dictionary<Tuple<TileType, TileType, TileType, TileType>, Vector2I> rules = new() {
		// Dirt And Grass
		{new (Hole, Hole, Hole, Hole), new Vector2I(0, 3)}, // DEFAULT
        {new (Hole, Hole, Hole, Grass), new Vector2I(1, 3)}, // OUTER_BOTTOM_RIGHT
        {new (Hole, Hole, Grass, Hole), new Vector2I(0, 0)}, // OUTER_BOTTOM_LEFT
        {new (Hole, Grass, Hole, Hole), new Vector2I(0, 2)}, // OUTER_TOP_RIGHT
        {new (Grass, Hole, Hole, Hole), new Vector2I(3, 3)}, // OUTER_TOP_LEFT
        {new (Hole, Grass, Hole, Grass), new Vector2I(1, 0)}, // EDGE_RIGHT
        {new (Grass, Hole, Grass, Hole), new Vector2I(3, 2)}, // EDGE_LEFT
        {new (Hole, Hole, Grass, Grass), new Vector2I(3, 0)}, // EDGE_BOTTOM
        {new (Grass, Grass, Hole, Hole), new Vector2I(1, 2)}, // EDGE_TOP
        {new (Hole, Grass, Grass, Grass), new Vector2I(1, 1)}, // INNER_BOTTOM_RIGHT
        {new (Grass, Hole, Grass, Grass), new Vector2I(2, 0)}, // INNER_BOTTOM_LEFT
        {new (Grass, Grass, Hole, Grass), new Vector2I(2, 2)}, // INNER_TOP_RIGHT
        {new (Grass, Grass, Grass, Hole), new Vector2I(3, 1)}, // INNER_TOP_LEFT
        {new (Hole, Grass, Grass, Hole), new Vector2I(2, 3)}, // DUAL_UP_RIGHT
        {new (Grass, Hole, Hole, Grass), new Vector2I(0, 1)}, // DUAL_DOWN_RIGHT
		{new (Grass, Grass, Grass, Grass), new Vector2I(2, 1)}, // No corners,

		{new (Water, Water, Water, Grass), new Vector2I(5, 3)}, // OUTER_BOTTOM_RIGHT
        {new (Water, Water, Grass, Water), new Vector2I(4, 0)}, // OUTER_BOTTOM_LEFT
        {new (Water, Grass, Water, Water), new Vector2I(4, 2)}, // OUTER_TOP_RIGHT
        {new (Grass, Water, Water, Water), new Vector2I(7, 3)}, // OUTER_TOP_LEFT
        {new (Water, Grass, Water, Grass), new Vector2I(5, 0)}, // EDGE_RIGHT
        {new (Grass, Water, Grass, Water), new Vector2I(7, 2)}, // EDGE_LEFT
        {new (Water, Water, Grass, Grass), new Vector2I(7, 0)}, // EDGE_BOTTOM
        {new (Grass, Grass, Water, Water), new Vector2I(5, 2)}, // EDGE_TOP
        {new (Water, Grass, Grass, Grass), new Vector2I(5, 1)}, // INNER_BOTTOM_RIGHT
        {new (Grass, Water, Grass, Grass), new Vector2I(6, 0)}, // INNER_BOTTOM_LEFT
        {new (Grass, Grass, Water, Grass), new Vector2I(6, 2)}, // INNER_TOP_RIGHT
        {new (Grass, Grass, Grass, Water), new Vector2I(7, 1)}, // INNER_TOP_LEFT
        {new (Water, Grass, Grass, Water), new Vector2I(6, 3)}, // DUAL_UP_RIGHT
        {new (Grass, Water, Water, Grass), new Vector2I(4, 1)}, // DUAL_DOWN_RIGHT
		{new (Water, Water, Water, Water), new Vector2I(4, 3)}, // No corners,
	};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (Vector2I coord in dataLayer.GetUsedCells())
		{
			SetDisplayTile(coord);
		}
	}

	public void SetTile(Vector2I coords, Vector2I atlasCoords)
	{
		dataLayer.SetCell(coords, 0, atlasCoords);
	}

	public void SetDisplayTile(Vector2I pos)
	{
		for (int i = 0; i < NEIGHTBOURS.Length; i++)
		{
			Vector2I newPos = pos + NEIGHTBOURS[i];

			displayLayer.SetCell(newPos, 0, CalculateDisplayTitle(newPos));
		}
	}

	public Vector2I CalculateDisplayTitle(Vector2I newCoords)
	{
		TileType botRight = GetWorldTile(newCoords - NEIGHTBOURS[0]);
		TileType botLeft = GetWorldTile(newCoords - NEIGHTBOURS[1]);
		TileType topRight = GetWorldTile(newCoords - NEIGHTBOURS[2]);
		TileType topLeft = GetWorldTile(newCoords - NEIGHTBOURS[3]);

		return rules[new(topLeft, topRight, botLeft, botRight)];
	}

	TileType GetWorldTile(Vector2I coords)
	{
		Vector2I atlasCoords = dataLayer.GetCellAtlasCoords(coords);

		if (atlasCoords == new Vector2I(-1, -1)) return Grass;

		string tileType = dataLayer.GetCellTileData(coords).GetCustomData("TileType").As<String>();

		return tileType switch
		{
			"Grass" => Grass,
			"Water" => Water,
			"Hole" => Hole,
			_ => Grass,
		};
	}
}