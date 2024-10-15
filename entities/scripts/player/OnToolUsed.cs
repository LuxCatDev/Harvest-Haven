using Component.State;
using Components;
using Godot;
using Items;

namespace Entities.Player;

public partial class OnToolUsed : StateEvent
{
	[Export]
	private EquipmentControllerComponent equipmentController;

	[Export]
	private Node2D itemWrapper;

	public override void _Ready()
	{
		equipmentController.Connect(EquipmentControllerComponent.SignalName.OnToolSpawned, Callable.From(OnToolSpawned));
	}

	public void OnToolSpawned()
	{
		if (itemWrapper.GetChild(0) is Tool tool)
		{
			tool.Connect(Tool.SignalName.OnUsed, Callable.From(OnUsed));
		}	
	}

	public void OnUsed()
	{
		Emit();
	}
}