using Godot;
using GodotUtilities;
using Items;

namespace UI;

[Scene]
public partial class ItemInformationPanel: Panel 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Signal]
	public delegate void OnItemFocusedChangedEventHandler();

	[Node]
	private Label itemName;

	[Node("Icon/Texture")]
	private TextureRect iconTextureRect;

	[Node("Panel/CoinIcon")]
	private TextureRect coinIcon;

	[Node("Panel/ItemValue")]
	private Label itemValue;

	[Node]
	private Label itemDescription;

	public Item CurrentItem;

	public void SetItem(Item item)
	{
		CurrentItem = item;

		itemName.Text = item.Name;

		iconTextureRect.Texture = item.Texture;

		coinIcon.Show();

		itemValue.Text = item.Value.ToString();

		itemDescription.Text = item.Description;

		EmitSignal(SignalName.OnItemFocusedChanged);
	}

}