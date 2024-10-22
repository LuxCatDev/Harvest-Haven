using System;
using Godot;
using GodotUtilities;

namespace Components;

[Scene]
public partial class TimeComponent : Node2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Export]
	private GradientTexture1D gradient;

	[Export]
	private CanvasModulate canvasModulate;

	[Export]
	private float inGameSpeed = 1.0f;

	[Signal]
	public delegate void TimeTickEventHandler(int day, int hour, int minute);

	const int MINUTES_PER_DAY = 1440;
	const int MINUTES_PER_HOUR = 60;
	const float INGAME_TO_REAL_MINUTE_DURATION = (2.0f * (float)Math.PI) / MINUTES_PER_DAY;

	private float time = 0.0f;
	private float pastMinute = -1.0f;

	public int Day = 1;
	public int Hour;
	public int Minutes;

	public override void _Ready()
	{
		time = INGAME_TO_REAL_MINUTE_DURATION * 12 * MINUTES_PER_HOUR;
	}

	public override void _Process(double delta)
	{
		time += (float)delta * INGAME_TO_REAL_MINUTE_DURATION * inGameSpeed;

		double value = Math.Sin(time - Math.PI / 2) + 1.0 / 2.0;

		canvasModulate.Color = gradient.Gradient.Sample((float)value);

		RecalculateTime();
	}

	private void RecalculateTime()
	{
		int minutes = (int)(time / INGAME_TO_REAL_MINUTE_DURATION);

		Hour = minutes / MINUTES_PER_HOUR;
		Minutes = minutes % MINUTES_PER_HOUR;

		if (pastMinute != Minutes)
		{
			if (minutes == MINUTES_PER_DAY)
			{
				pastMinute = -1;
				time = 0;
				Day++;
			} else {
				pastMinute = Minutes;
			}
			EmitSignal(SignalName.TimeTick, Day, Hour, Minutes);
		}
	}
}