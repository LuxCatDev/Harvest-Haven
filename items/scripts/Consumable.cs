using Godot;
using System;

namespace Items;

[GlobalClass]
public partial class Consumable : ItemData
{
	[Export]
    public int FoodMod;

    [Export]
    public int WaterhMod;

    [Export]
    public int EnergyMod;

    Consumable(): this(0,0,0) {}

    public Consumable(int foodMod, int waterhMod, int energyMod)
    {
        FoodMod = foodMod;
        WaterhMod = waterhMod;
        EnergyMod = energyMod;
    }
}
