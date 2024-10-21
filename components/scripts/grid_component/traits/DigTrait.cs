using Godot;
using Models;
using Objects;

namespace Components.GridComponent.Traits;

[GlobalClass]
public partial class DigTrait : Trait
{
    [Export]
    private PlacesableObject soilResource;

    public override bool IsValid()
    {
        return GridValidationComponent.IsValid;
    }

    public override void Execute()
    {
        Soil objectInstance = soilResource.Scene.Instantiate<Soil>();
        objectInstance.collisionShape.Disabled = false;
        objectInstance.GlobalPosition = GridValidationComponent.Crosshair.CenterGlobalPosition;

        GameManager.Instance.Level.AddChild(objectInstance);
        Vector2I tilePosition = GridValidationComponent.Crosshair.TilePosition;
        GameManager.Instance.TerrainLayer.SetCell(tilePosition, 0, new Vector2I(2, 0));
        GameManager.Instance.TileMapSystem.SetDisplayTile(tilePosition);
    }
}