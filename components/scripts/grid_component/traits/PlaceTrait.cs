using Godot;
using Models;
using Objects;

namespace Components.GridComponent.Traits;

[GlobalClass]
public partial class PlaceTrait : Trait
{
  public override bool IsValid()
  {
    if (!PlacingControllerComponent.IsActive) return false;
    return GridValidationComponent.IsValid;
  }

  public override void Execute()
  {
    Object objectInstance = PlacingControllerComponent.PlacesableObject.Scene.Instantiate<Object>();
    objectInstance.collisionShape.Disabled = false;
    objectInstance.GlobalPosition = PlacingControllerComponent.GridValidation.Crosshair.CenterGlobalPosition;

    GameManager.Instance.Level.AddChild(objectInstance);
  }
}