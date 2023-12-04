using Godot;
using System;

public partial class fix_wire : Area2D
{

	private bool interact = false;
	private bool isShow = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (interact == false)
		{
			
		}
		else
		{
			if (Input.IsActionPressed("ui_accept"))
			{
				GD.Print("Espace");

			}
		}
	}

	private void _on_body_entered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			interact = true;
			GD.Print(interact);
		}
	}

	private void _on_body_exited(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			interact = false;
			GD.Print(interact);
		}

	}

}
