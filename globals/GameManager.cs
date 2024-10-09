using Common;
using Entities.Player;
using Godot;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	public Player Player { get; set; }
	public PlacementLayer PlacementLayer { get; set; }

	public Crosshair Crosshair { get; set; }

	public DialogController DialogController { get; set; }

    public override void _Ready()
    {
		Instance = this;
    }
}
