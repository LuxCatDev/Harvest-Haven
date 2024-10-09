using Godot;
using System;

public partial class DialogController : Node
{
	private CanvasLayer actualDialog;

	public void ShowDialog(CanvasLayer dialog)
	{
		actualDialog?.Hide();

		actualDialog = dialog;

		actualDialog.Show();
	}

	public void HideDialog()
	{
		actualDialog.Hide();
	}

    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("exit"))
		{
			if (actualDialog != null)
			{
				GetTree().Paused = false;
				HideDialog();
			}
		}
    }
}
