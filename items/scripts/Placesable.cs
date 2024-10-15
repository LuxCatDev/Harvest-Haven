using Godot;
using Models;
using System;

namespace Items;

[GlobalClass]
public partial class Placesable : ItemData
{
	[Export]
    public PlacesableObject PlacesableObject;

    Placesable(): this(null) {}

    public Placesable(PlacesableObject placesableObject)
    {
        PlacesableObject = placesableObject;
    }
}
