using Entities.Player;
using Godot;

namespace Components.GridComponent.Traits;

[GlobalClass]
public partial class DamageTrait : Trait
{

    [Export]
    public HurtBoxType Type;

    [Export]
    public int Damage;

    public override bool IsValid()
    {
        if (!ToolControllerComponent.IsActive) return false;

        return GridValidationComponent.IsValid;
    }

    public override void Execute()
    {
		ToolControllerComponent.HurtBox.Type = Type;
        ToolControllerComponent.HurtBox.Damage = Damage;
        ToolControllerComponent.HurtBox.Shoot();
    }
}