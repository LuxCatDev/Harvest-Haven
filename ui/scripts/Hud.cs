using Godot;
using GodotUtilities;

namespace UI;

[Scene]
public partial class Hud: CanvasLayer 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node]
	private Label dayLabel;

	[Node]
	private Label timeLabel;

    public void Init()
    {
		GameManager.Instance.TimeComponent.TimeTick += (int day, int hour, int minute) => {
			dayLabel.Text = "Day: " + day;
			timeLabel.Text = "Time: " + hour + ":" + minute;
		};
    }
}