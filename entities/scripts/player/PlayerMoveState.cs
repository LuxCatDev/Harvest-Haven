using Components.State;
using Godot;

namespace Entities.Player;

public partial class PlayerMoveState: State
{

    [Export]
    private int moveSpeed = 100;

    public override void Enter() {
        player.UpdateAnimation("move");
    }
    public override void Exit() {

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