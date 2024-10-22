using Common;
using Entities.Player;
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

	[Export]
	private float itemDropVelocity = 75;

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
				SpawnItem(new(items[itemIndex].Item, items[itemIndex].Amount));
			}
		}

		EmitSignal(SignalName.OnDropFinished);
	}

	private void SpawnItem(InventoryItem item)
	{
		PickableItem pickableItem = itemScene.Instantiate<PickableItem>();

		pickableItem.InventoryItem = item;
		pickableItem.GlobalPosition = GlobalPosition;
		pickableItem.Velocity = Vector2.Right.Rotated(randomNumberGenerator.RandfRange(-1.5f, 1.5f)) * itemDropVelocity * randomNumberGenerator.RandfRange(0.5f, 1.0f);

		GameManager.Instance.Level.AddChild(pickableItem);

		pickableItem.animationPlayer.Play("bounce");
	}
}
