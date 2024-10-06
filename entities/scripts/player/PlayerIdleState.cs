using Components.State;
using Godot;

namespace Entities.Player;

public partial class PlayerIdleState: State
{
    public override void Enter() {
        player.UpdateAnimation("idle");
    }
    public override void Exit() {

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