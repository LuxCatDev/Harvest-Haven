using Common;
using Entities.Player;
using Godot;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	public Player Player { get; set; }

	public TileMapLayer TerrainLayer { get; set; }

	public DialogController DialogController { get; set; }

	public Node2D Level { get; set; }

	public TileMapSystem TileMapSystem { get; set; }

    public override void _Ready()
    {
		Instance = this;
    }
}
