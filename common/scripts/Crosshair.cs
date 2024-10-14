using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Player;
using Godot;
using GodotUtilities;

namespace Common;

[Scene]
public partial class Crosshair : Node2D
{

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private Panel free;

	[Node]
	private Panel restricted;

	[Export]
	public Vector2 Size = new(16, 16);

	public Vector2I TilePosition;

	public Vector2 Center;

	public Vector2 CenterGlobalPosition
	{
		get => ToGlobal(Center);
		private set { }
	}

	public float DistanceToPlayer
	{
		get
		{
			Player player = GameManager.Instance.Player;

			return (CenterGlobalPosition + (CenterGlobalPosition.DirectionTo(player.GlobalPosition) * Size / 2)).DistanceTo(player.GlobalPosition);
		}
		private set { }
	}

	public bool State
	{
		set
		{
			if (value)
			{
				free.Visible = true;

				restricted.Visible = false;
			}
			else
			{
				free.Visible = false;

				restricted.Visible = true;
			}
		}
	}

	public void ShowCrosshair()
	{
		Visible = true;
		free.Size = Size;
		restricted.Size = Size;
		Center = new(-8 + (Size.X / 16 * 8), -8 + (Size.Y / 16 * 8));
	}

	public void HideCrosshair()
	{
		Visible = false;
	}

	public override void _Process(double delta)
	{
		if (!Visible) return;

		TileMapLayer map = GameManager.Instance.TerrainLayer;

		Vector2 mousePosition = map.ToLocal(GetGlobalMousePosition());

		Vector2I mapPosition = map.LocalToMap(mousePosition);
		Vector2 tile = map.MapToLocal(mapPosition);

		TilePosition = mapPosition;

		GlobalPosition = tile;
	}

	public List<Vector2I> GetTargetetCells()
	{
		TileMapLayer map = GameManager.Instance.TerrainLayer;
		Vector2I firstCell = map.LocalToMap(free.GlobalPosition);
		Vector2I lastCell = map.LocalToMap(free.GlobalPosition + Size - Center);

		Vector2 cellCount = Size / 16;
		List<Vector2I> cells = new();

		foreach (int x in Enumerable.Range(0, (int)cellCount.X))
		{
			foreach (int y in Enumerable.Range(0, (int)cellCount.Y))
			{
				cells.Add(new Vector2I(firstCell.X + x, firstCell.Y + y));
			}
		}

		return cells;
	}
}
