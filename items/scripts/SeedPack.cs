using Godot;
using Models;
using System;

namespace Items;

[GlobalClass]
public partial class SeedPack : ItemData
{
	[Export]
    public PlacesableObject PlacesableObject;

    SeedPack(): this(null) {}

    public SeedPack(PlacesableObject placesableObject)
    {
        PlacesableObject = placesableObject;
    }
}
