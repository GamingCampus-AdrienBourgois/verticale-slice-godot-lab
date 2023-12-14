using Godot;
using System;

public partial class Trashcan : StaticBody2D
{
	private int Trash_collected = 0;
	private int TrashToCollect = 0;
	public bool Full = false;

	private AudioStreamPlayer2D audio = null;

	// Getters
	private int GetTrash_collected() { return Trash_collected; }
	public override void _Ready()
	{
		audio = GetNode<AudioStreamPlayer2D>("Audio");
		foreach (Node i in GetParent().GetNode<Node>("Bananas").GetChildren())
		{
			TrashToCollect++;
		}
		
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
			audio.Play();
			Trash_collected++;
			if (Trash_collected >= TrashToCollect){
				Full = true;
				GD.Print("Trashcan full !");
			}
		}
	}


}



