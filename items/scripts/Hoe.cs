using Godot;
using GodotUtilities;
using Items;
using Models;
using Objects;

namespace Items;

[Scene]
public partial class Hoe : Tool
{
    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes(); // this is a generated method
        }
    }

    [Export]
    private PlacesableObject soilResource;

    public override void _Ready()
    {
        equipmentController.GridValidation.PlacesableObject = soilResource;
        equipmentController.GridValidation.CrosshairSize = new(1,1);
        equipmentController.GridValidation.Enable();
    }

    public void Dig()
    {
        if (equipmentController.GridValidation.IsValid)
        {
            Soil objectInstance = soilResource.Scene.Instantiate<Soil>();
            objectInstance.collisionShape.Disabled = false;
            objectInstance.GlobalPosition = equipmentController.GridValidation.Crosshair.CenterGlobalPosition;

            GameManager.Instance.Level.AddChild(objectInstance);
            Vector2I tilePosition = equipmentController.GridValidation.Crosshair.TilePosition;
            GameManager.Instance.TerrainLayer.SetCell(tilePosition, 0, new Vector2I(2, 0));
            GameManager.Instance.TileMapSystem.SetDisplayTile(tilePosition);
        }
    }
}