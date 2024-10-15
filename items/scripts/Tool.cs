using Components;
using Godot;
using GodotUtilities;

namespace Items;

[Scene]
public partial class Tool : Node2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Signal]
	public delegate void OnUsedEventHandler();

	[Export]
	private AnimationPlayer animationPlayer;

	[Export]
	private HurtBoxComponent hurtBox;

	[Export]
	public EquipmentControllerComponent equipmentController;

	public override void _Process(double delta)
	{
		TileMapLayer map = GameManager.Instance.TerrainLayer;

		Vector2 position = map.ToLocal(GameManager.Instance.Player.GlobalPosition + GameManager.Instance.Player.CardinalDirection * 16);

		Vector2I mapPosition = map.LocalToMap(position);
		Vector2 tile = map.MapToLocal(mapPosition);

		if (hurtBox != null)
		{
			hurtBox.GlobalPosition = tile;
		}
	}

	public void Use()
	{
		EmitSignal(SignalName.OnUsed);
		animationPlayer.Play("use_" + GameManager.Instance.Player.AnimationDirection);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("primary_action"))
		{
			Use();
		}
	}
}