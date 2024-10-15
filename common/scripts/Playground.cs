using System.Collections.Generic;
using System.Linq;
using Entities.Player;
using Godot;
using GodotUtilities;
using Models;
using Objects;

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
	private TileMapSystem tileMapSystem;

	[Node]
	private TileMapLayer terrainLayer;

	[Node]
	private Player player;

	[Node]
	private DialogController dialogController;

	[Export]
	private PlacesableObject placesableObject;

    public override void _Ready()
    {
		GameManager.Instance.Player = player;
		GameManager.Instance.Level = this;
		GameManager.Instance.DialogController = dialogController;
		GameManager.Instance.TerrainLayer = terrainLayer;
		GameManager.Instance.TileMapSystem = tileMapSystem;
    }

	public void SpawnNode(Node2D node)
	{
		AddChild(node);
	}
}