using Common;
using Godot;
using System;
using System.Linq;

namespace Components;

public partial class DropperComponent : Node2D
{
	[Signal]
	public delegate void OnDropFinishedEventHandler();

	[Export]
	private Godot.Collections.Array<InventoryItem> items;

	[Export]
	private int maxNumberOfDrops;

	[Export]
	private int minNumberOfDrops;

	[Export]
	private bool random;

	private PackedScene itemScene = GD.Load<PackedScene>("res://common/scenes/pickable_item.tscn");

	private RandomNumberGenerator randomNumberGenerator = new();

	public void Drop()
	{
		if (!random)
		{
			foreach (InventoryItem item in items)
			{
				SpawnItem(item);
			}
		}
		else
		{
			int dropCount = randomNumberGenerator.RandiRange(minNumberOfDrops, maxNumberOfDrops);

			foreach (int i in Enumerable.Range(0, dropCount))
			{
				int itemIndex = randomNumberGenerator.RandiRange(0, items.Count - 1);

				SpawnItem(items[itemIndex]);
			}
		}

		EmitSignal(SignalName.OnDropFinished);
	}

	private void SpawnItem(InventoryItem item)
	{
		PickableItem pickableItem = itemScene.Instantiate<PickableItem>();

		pickableItem.InventoryItem = item;
		pickableItem.velocity = Vector2.Right.Rotated(randomNumberGenerator.Randf() * 2.0f * MathF.PI) * 50 * randomNumberGenerator.RandfRange(0.5f, 1);

		GameManager.Instance.Level.AddChild(pickableItem);

		GD.Print(GlobalPosition);
		
		pickableItem.GlobalPosition = GlobalPosition;
	}
}
