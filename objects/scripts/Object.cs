using Components;
using Godot;
using GodotUtilities;

namespace Objects;

[Scene]
public partial class Object: StaticBody2D 
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		} 
	}

	[Node("CollisionShape2D")]
	public CollisionShape2D collisionShape;

	[Node("PlacingAreaComponent")]
	public PlacingAreaComponent placingArea;
}