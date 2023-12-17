using Godot;
using System;


public partial class Pause : Control
{
	private bool paused = false;

	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("Pause"))
		{
			
		}
	}

	public void PauseMenu()
	{
		if (paused)
		{
			this.Hide();
			Engine.TimeScale = 1;
		}
		else 
		{
			this.Show();
			Engine.TimeScale = 0;
		}
		paused = !paused;
	}
}
