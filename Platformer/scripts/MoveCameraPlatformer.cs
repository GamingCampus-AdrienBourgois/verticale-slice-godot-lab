using Godot;
using System;

public partial class MoveCameraPlatformer : Marker2D
{
	[Export]
	private int Mode_Camera = 0;
	public CharacterBody2D player = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Mode_Camera == 0){
			GetNode<Timer>("Timer").QueueFree();
		}
		player = GetParent().GetNode<CharacterBody2D>("player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var newPosition = Position;
		newPosition.X = player.Position.X;
		Position = newPosition;

		if (Mode_Camera == 1){
			RotationDegrees++;
		}

	}
	private void _on_timer_timeout()
	{
		if (Mode_Camera == 2){
			Random rnd = new Random();
			Rotation = rnd.Next(0, 360);
		}
	}

}


