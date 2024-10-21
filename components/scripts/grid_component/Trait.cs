using Common;
using Godot;

namespace Components.GridComponent;

[GlobalClass]
public partial class Trait: Resource
{
    public GridValidationComponent GridValidationComponent;

    public PlacingControllerComponent PlacingControllerComponent;

    public ToolControllerComponent ToolControllerComponent;

    public void Init()
    {
        GridValidationComponent = GameManager.Instance.Player.GridValidationComponent;
        PlacingControllerComponent = GameManager.Instance.Player.PlacingControllerComponent;
        ToolControllerComponent = GameManager.Instance.Player.ToolControllerComponent;
    }
    
    public virtual bool IsValid()
    {
        return true;
    }

    public virtual void Execute()
    {

    }
}