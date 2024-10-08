using Godot;
using GodotUtilities;

namespace UI;

[Scene]
public partial class UI: CanvasLayer
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node("Viewport/Menu")]
	private Panel menu;

    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("open_menu"))
		{
			GD.Print(menu);
			menu.Visible = !menu.Visible;
		}
    }
}