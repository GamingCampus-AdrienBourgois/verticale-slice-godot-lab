using Godot;
using System;

public partial class MoveCameraPlatformer : Marker2D
{
	public CharacterBody2D player = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent().GetNode<CharacterBody2D>("player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var newPosition = Position;
		newPosition.X = player.Position.X;
		Position = newPosition;
	}
}
