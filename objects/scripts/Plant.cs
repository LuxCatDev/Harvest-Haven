using Godot;
using GodotUtilities;
using Objects;

namespace Objects;

[Scene]
public partial class Plant: Object 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}
}