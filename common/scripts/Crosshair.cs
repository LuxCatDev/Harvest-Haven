using Entities.Player;
using Godot;
using GodotUtilities;

namespace Common;

[Scene]
public partial class Crosshair: Node2D 
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

	[Node("Area2D/CollisionShape2D")]
	private CollisionShape2D collisionShape2D;

	[Node]
	private Area2D area2D;

	[Export]
	public Vector2 Size = new(16, 16);

	public Vector2I tilePosition;

	public bool IsOnPlayer = false;

	public bool IsOnRestrictedCell = false;

	public bool State {
		set {
			if (value)
			{
				free.Visible = true;

				restricted.Visible = false;
			} else {
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
		area2D.Position = new Vector2(-8 + (Size.X / 16 * 8), -8 + (Size.Y / 16 * 8));
		collisionShape2D.Shape.Set("size", new Vector2(Size.X - 1, Size.Y - 1));
	}

	public void HideCrosshair()
	{
		Visible = false;
	}
	
    public override void _Process(double delta)
    {
		if (!Visible) return;

		TileMapLayer map = GameManager.Instance.PlacementLayer.TileMapLayer;

		Vector2 mousePosition = map.ToLocal(GetGlobalMousePosition());

		Vector2I mapPosition = map.LocalToMap(mousePosition);
		Vector2 tile = map.MapToLocal(mapPosition);

		tilePosition = mapPosition;

		GlobalPosition = tile;
    }

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			IsOnPlayer = true;

			return;
		}

		IsOnRestrictedCell = true;
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is Player)
		{
			IsOnPlayer = false;

			return;
		}

		IsOnRestrictedCell = false;
	}
}
