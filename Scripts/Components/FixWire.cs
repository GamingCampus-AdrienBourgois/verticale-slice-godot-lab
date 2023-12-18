using Godot;
using System;

public partial class FixWire : Area2D
{
	private Player player;
	private CanvasLayer fixWireHud;
	private MenuCursor menuCursor;
	private bool interact = false;
	private bool isShow = false;
	public bool WireIsFinish = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		player = GetParent().GetNode<Player>("Player");
		fixWireHud = GetNode<CanvasLayer>("Fix_wire_hud");
		menuCursor = fixWireHud.GetNode<MenuCursor>("Menu_cursor");
		if (fixWireHud != null)
		{
			fixWireHud.Visible = isShow;
		}
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Interact") && interact && !WireIsFinish)
		{
			ShowWireHud();
		}
	}

	private void ShowWireHud()
	{
		if (!isShow)
		{
			isShow = true;
			player.inputOnFocus = true;
			fixWireHud.Visible = isShow;
			menuCursor.isWiring = true;
		}
		else
		{
			isShow = false;
			player.inputOnFocus = false;
			fixWireHud.Visible = isShow;
		}
	}

	public void ShowText()
	{
		if (interact)
		{
			player.to_label.Text = "E to interact";
			player.ui_animations.Play("appear");
		}
		else if (!interact)
		{
			player.ui_animations.PlayBackwards("appear");
		}
	}

	private void _on_body_entered(Node2D body)
	{
		if (body.IsInGroup("Player") && !WireIsFinish)
		{
			interact = true;
			ShowText();
		}
	}

	private void _on_body_exited(Node2D body)
	{
		if (body.IsInGroup("Player") && !WireIsFinish)
		{
			interact = false;
			ShowText();
		}

	}

	public void WireIsFix()
	{
		ShowWireHud();
		ShowText();
		GD.Print("Cable Reparer");
		// Faire plus propre avec un signal si on veut
		Node WireDoors = GetParent().GetNode("Doors").GetNode("WireDoors");
		foreach(door i in WireDoors.GetChildren())
		{
			i.DoorOpenedChange();
		}
	}
}
