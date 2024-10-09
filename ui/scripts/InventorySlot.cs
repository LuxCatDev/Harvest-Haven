using Components.InventoryComponent;
using Godot;
using GodotUtilities;

namespace UI;

[Scene]
public partial class InventorySlot: Control 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node("Icon/Texture")]
	private TextureRect texture;

	[Node("Panel/Amount")]
	private Label amount;

	[Node]
	private Label accessNumber;

	public int Index;

	public InventoryItem InventoryItem { get; private set; }

	public InventoryComponent Inventory;

    public override Variant _GetDragData(Vector2 atPosition)
    {
		if (InventoryItem == null) return default;

        TextureRect preview = new()
        {
            Texture = InventoryItem.Item.Texture,
			Size = new(16, 16),
			ZIndex = 100
        };

        SetDragPreview(preview);

		return this;
    }
    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
		InventorySlot inventorySlot = data.As<InventorySlot>();
        if (inventorySlot != null)
        {
			return true;
        }

		return false;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
		InventorySlot inventorySlot = data.As<InventorySlot>();
		
		if (InventoryItem != null)
		{
			if (InventoryItem.Item == inventorySlot.InventoryItem.Item) {
				Inventory.AddItem(inventorySlot.InventoryItem);
				inventorySlot.Inventory.RemoveItemAt(inventorySlot.Index);
			} else {
				InventoryItem temp = InventoryItem;
				InventoryItem temp2 = inventorySlot.InventoryItem;

				Inventory.SetItem(temp2, Index);
				inventorySlot.Inventory.SetItem(temp, inventorySlot.Index);
			}
		} else {
			Inventory.SetItem(inventorySlot.InventoryItem, Index);
			inventorySlot.Inventory.RemoveItemAt(inventorySlot.Index);
		}
    }

    public void SetItem(InventoryItem inventoryItem)
	{
		InventoryItem = inventoryItem;
		texture.Texture = inventoryItem.Item.Texture;
		amount.Text = inventoryItem.Amount.ToString();	
	}

	public void SetEmpty()
	{
		InventoryItem = null;
		texture.Texture = null;
		amount.Text = "";
	}

	public void SetIndex(int index)
	{
		accessNumber.Text = index <= 7 ? (index + 1).ToString() : "";
		Index = index;
	}
}