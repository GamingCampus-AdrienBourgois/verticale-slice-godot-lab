using Godot;
using System;

public partial class fix_wire : Area2D
{
	private Player player;
	private CanvasLayer fixWireHud;
	private bool interact = false;
	private bool isShow = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		player = GetParent().GetNode<Player>("Player");
		fixWireHud = GetNode<CanvasLayer>("Fix_wire_hud");
		if (fixWireHud != null)
		{
			fixWireHud.Visible = isShow;
			
		}
		GD.Print(fixWireHud.Visible, "     ", fixWireHud);
	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (interact == false)
		{
			
		}
		else
		{
			if (Input.IsActionJustPressed("interact") && isShow == false)
			{
				isShow = true;
				player.inputOnFocus = true;
				fixWireHud.Visible = isShow;

			} else if (Input.IsActionJustPressed("ui_cancel") && isShow == true)
			{
				isShow = false;
				player.inputOnFocus = false;
				fixWireHud.Visible = isShow;
			}
		}
	}

	private void _on_body_entered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			interact = true;
		}
	}

	private void _on_body_exited(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			interact = false;
		}

	}

	public void WireIsFix()
	{
		isShow = false;
		player.inputOnFocus = false;
		fixWireHud.Visible = isShow;
		GD.Print("Cable Reparer");
	}

}
