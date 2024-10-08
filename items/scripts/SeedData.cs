using Godot;
using Models;
using System;

namespace Items;

[GlobalClass]
public partial class SeedData : ItemData
{
	[Export]
	public PlacesableObject Object;

	SeedData(): this(null) {}

	public SeedData(PlacesableObject placesableObject)
	{
		Object = placesableObject;
	}
}
