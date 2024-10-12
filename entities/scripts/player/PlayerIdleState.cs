using Components.State;
using Godot;
using Namespace;

namespace Entities.Player;

public partial class PlayerIdleState: State
{

    [Export]
    public EquipmentControllerComponent equipmentControllerComponent;

    public override void Enter() {
        player.UpdateAnimation("idle");
        equipmentControllerComponent.OnEquipmentChanged += OnEquipmentChanged;
    }
    public override void Exit() {
        equipmentControllerComponent.OnEquipmentChanged -= OnEquipmentChanged;
    }

    public void OnEquipmentChanged()
    {
        player.UpdateAnimation("idle");
    }
    public override void HandleProcess(double delta) {
        if (player.Direction != Vector2.Zero)
        {
            EmitEvent("OnPlayerMove");
        }

        player.Velocity = Vector2.Zero;
    }
    public override void HandlePhysicsProcess(double delta) {

    }
    public override void HandleInput(InputEvent @event) {

    }
}