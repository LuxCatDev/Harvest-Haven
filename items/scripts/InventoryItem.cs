using Godot;
using Items;
using System;

[GlobalClass]
public partial class InventoryItem : Resource
{
	[Export]
	public Item Item;

	[Export]
	public int Amount;

	InventoryItem(): this(null, 0) {}

	public InventoryItem(Item item, int amount)
	{
		Item = item;
		Amount = amount;
	}	
}
