using Godot;
using System;

namespace Items;

public enum ItemType
{
	Item,
	Product,
	Tool,
	Seed,
}

[GlobalClass]
public partial class Item : Resource
{
	[Export]
	public string Name;
	
	[Export]
	public string Description;

	[Export]
	public AtlasTexture Texture;

	[Export]
	public int Value;

	[Export]
	public ItemType Type;

	[Export]
	public ItemData Data;

	Item() : this("", "", null, 0, ItemType.Item, null) {}

	public Item(string name, string description, AtlasTexture texture, int value, ItemType type, ItemData data)
	{
		Name = name;
		Description = description;
		Texture = texture;
		Value = value;
		Type = type;
		Data = data;
	}
}

