using Components;
using Components.State;
using Godot;
using Items;

namespace Entities.Player;

public partial class PlayerUsingState: State
{
    [Export]
    public EquipmentControllerComponent equipmentControllerComponent;

    public override void Enter() {
		player.Velocity = Vector2.Zero;

		equipmentControllerComponent.Active = false;

		if (equipmentControllerComponent.CurrentItem.Item.Data is ToolData data)
		{
			switch (data.Type)
			{
				case ToolType.Axe:
					player.UpdateAnimation("axe");
					break;
				case ToolType.WateringCan:
					player.UpdateAnimation("water");
					break;
				default:
					player.UpdateAnimation("tool");
					break;
			}

			player.AnimationPlayer.AnimationFinished += OnAnimationFinished;
		} else {
			EmitEvent("OnUsingEnded");
		}
    }

	public void OnAnimationFinished(Godot.StringName anim)
	{
		EmitEvent("OnUsingFinished");
	}

    public override void Exit() {
		player.AnimationPlayer.AnimationFinished -= OnAnimationFinished;
		equipmentControllerComponent.Active = true;
    }

    public override void HandleProcess(double delta) {
        player.Velocity = Vector2.Zero;
    }
    public override void HandlePhysicsProcess(double delta) {

    }
    public override void HandleInput(InputEvent @event) {

    }
}