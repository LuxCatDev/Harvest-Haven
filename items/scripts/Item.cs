using Godot;
using System;

namespace Items;

public enum ItemType
{
	Item,
	Product,
	Tool,
}

[GlobalClass]
public partial class Item : Resource
{
	[Export]
	public string Name;
	
	[Export]
	public string Description;

	[Export]
	public CompressedTexture2D Texture;

	[Export]
	public int Value;

	[Export]
	public bool Stackable = true;

	[Export]
	public ItemType Type;

	[Export]
	public ItemData Data;

	Item() : this("", "", null, 0, ItemType.Item, null, true) {}

	public Item(string name, string description, CompressedTexture2D texture, int value, ItemType type, ItemData data, bool stackable)
	{
		Name = name;
		Description = description;
		Texture = texture;
		Value = value;
		Type = type;
		Data = data;
		Stackable = stackable;
	}
}

