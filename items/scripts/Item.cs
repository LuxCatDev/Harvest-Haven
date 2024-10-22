using Godot;
using System;

namespace Items;

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
	public ItemData Data;

	Item() : this("", "", null, 0, null, true) {}

	public Item(string name, string description, CompressedTexture2D texture, int value, ItemData data, bool stackable)
	{
		Name = name;
		Description = description;
		Texture = texture;
		Value = value;
		Data = data;
		Stackable = stackable;
	}
}

