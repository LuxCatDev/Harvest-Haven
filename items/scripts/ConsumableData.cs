using Godot;
using System;

namespace Items;

[GlobalClass]
public partial class ConsumableData : ProductData
{
	[Export]
    public int FoodMod;

    [Export]
    public int WaterhMod;

    [Export]
    public int EnergyMod;

    ConsumableData(): this(0,0,0) {}

    public ConsumableData(int foodMod, int waterhMod, int energyMod)
    {
        FoodMod = foodMod;
        WaterhMod = waterhMod;
        EnergyMod = energyMod;
    }
}
