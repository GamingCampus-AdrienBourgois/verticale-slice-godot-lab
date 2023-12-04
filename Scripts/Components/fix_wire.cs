using Godot;
using System;

public partial class fix_wire : Area2D
{

	private bool interact = false;
	private bool isShow = false;
	private CanvasLayer fixWireHud;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fixWireHud = GetNode<CanvasLayer>("Fix_wire_hud");
		if (fixWireHud != null)
		{
			fixWireHud.Visible = isShow;
			GD.Print("OK");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (interact == false)
		{
			
		}
		else
		{
			if (Input.IsActionJustPressed("ui_accept") && isShow == false)
			{
				GD.Print("Espace");
				isShow = true;
				fixWireHud.Visible = isShow;

			} else if (Input.IsActionJustPressed("ui_left") && isShow == true){
				GD.Print("A plus");
				isShow = false;
				fixWireHud.Visible = isShow;
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
