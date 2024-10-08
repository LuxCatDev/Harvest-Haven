using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Components.InventoryComponent;

public partial class InventoryComponent: Node
{

    [Signal]
    public delegate void UpdatedEventHandler();

    [Export]
    public int Size;

    public List<InventoryItem> items;

    public override void _Ready()
    {
        items = new();

        foreach(int i in Enumerable.Range(0, Size))
        {
            items.Add(null);
        }
    }

    public bool IsFull {
        get {
            var res = from item in items where item != null select item;

            return res.Count() >= Size;
        }
    }

    public int FreeSlotIndex
    {
        get {
            int index = items.FindIndex((item) => item == null);

            return index;
        }
    }

    public void AddItem(InventoryItem inventoryItem)
    {
        if (IsFull) {
            int itemIndex = items.FindIndex((item) => item.Item == inventoryItem.Item);

            if (itemIndex != -1)
            {
                items[itemIndex].Amount += inventoryItem.Amount;
                EmitSignal(SignalName.Updated);
            }
        } else {
            int itemIndex = items.FindIndex((item) => item != null && item.Item == inventoryItem.Item);


            if (itemIndex != -1)
            {
                items[itemIndex].Amount += inventoryItem.Amount;
            } else if (FreeSlotIndex != -1)
            {
                items[FreeSlotIndex] = inventoryItem;
            }

            EmitSignal(SignalName.Updated);
        }
    }

    public void RemoveItemAt(int index)
    {
        items[index] = null;

        EmitSignal(SignalName.Updated);
    }

    public void SetItem(InventoryItem inventoryItem, int index)
    {
        items[index] = inventoryItem;

        EmitSignal(SignalName.Updated);
    }

    public void AddItemAt(InventoryItem inventoryItem, int index)
    {
        InventoryItem slot = items[index];

        if (slot != null)
        {
            items[index] = inventoryItem;
            EmitSignal(SignalName.Updated);
        } else if(slot.Item == inventoryItem.Item)
        {
            items[index].Amount += inventoryItem.Amount;
            EmitSignal(SignalName.Updated);
        }
    }
}