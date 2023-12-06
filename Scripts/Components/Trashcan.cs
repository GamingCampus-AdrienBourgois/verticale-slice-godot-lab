using Godot;
using System;

public partial class Trashcan : StaticBody2D
{
	private int Trash_collected = 0;
	private bool Full = false;

	// Getters
	private int GetTrash_collected() { return Trash_collected; }
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_area_2d_body_entered(Node2D body)
	{
		// Ne détecte que si c'est un characterbody et aps un staticbody

		if (body.IsInGroup("Trash") && Full != true){
			GD.Print("Trash !");
			body.QueueFree();
			Trash_collected++;
			if (Trash_collected >= 3){
				Full = true;
				GD.Print("Trashcan full !");
			}
		}
	}


}



