using Components;
using Components.State;
using Godot;

namespace Entities.Player;

public partial class PlayerMoveState: State
{

    [Export]
    private int moveSpeed = 100;

    [Export]
    public EquipmentControllerComponent equipmentControllerComponent;

    public override void Enter() {
        equipmentControllerComponent.OnEquipmentChanged += OnEquipmentChanged;
        player.UpdateAnimation("move");
    }
    public override void Exit() {
        equipmentControllerComponent.OnEquipmentChanged -= OnEquipmentChanged;
    }

    public void OnEquipmentChanged()
    {
        player.UpdateAnimation("move");
    }
    public override void HandleProcess(double delta) {
        if (player.Direction == Vector2.Zero)
        {
            EmitEvent("OnPlayerStopMoving");
        }

        player.Velocity = player.Direction * moveSpeed;

        if (player.SetDirection())
        {
            player.UpdateAnimation("move");
        }
    }
    public override void HandlePhysicsProcess(double delta) {

    }
    public override void HandleInput(InputEvent @event) {

    }
}