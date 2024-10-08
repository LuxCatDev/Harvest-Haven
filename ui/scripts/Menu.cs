using Godot;
using GodotUtilities;
using UI;

[Scene]
public partial class Menu : CanvasLayer
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Export]
	private PackedScene pageScene;

	private Control actualPage;

	[Node]
	private Control page;

	public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("open_menu"))
		{
			if (actualPage == null)
			{
				actualPage = pageScene.Instantiate<Control>();

				page.AddChild(actualPage);
			}
			Visible = !Visible;
			// GetTree().Paused = !GetTree().Paused;
		}
    }
}
