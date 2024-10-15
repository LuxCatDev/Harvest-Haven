using Godot;
using GodotUtilities;

namespace Objects;

[Scene]
public partial class Soil: Object
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	public bool watered;

	public Plant Plant { get; private set; }

	public void SetPlant(Plant plant)
	{
		GD.Print(plant);
		// plant.GlobalPosition = GlobalPosition;
		Plant = plant;
		AddChild(plant);
	}

	public void SetEmpty()
	{

	}

	public void SetWatered()
	{
		
	}

	public void SetDry()
	{
		
	}
}