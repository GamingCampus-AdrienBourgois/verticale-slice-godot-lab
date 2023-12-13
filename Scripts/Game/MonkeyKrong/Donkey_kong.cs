using Godot;
using System;

public partial class Donkey_kong : Node2D
{
	private void _on_destroy_body_entered(Node2D body)
	{
		body.QueueFree();
		GD.Print("Barril Freed !");
	}
}



