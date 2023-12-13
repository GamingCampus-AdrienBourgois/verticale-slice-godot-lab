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
		if (interact == true)
		{
			if (Input.IsActionJustPressed("Interact") && isShow == false)
			{
				isShow = true;
				player.inputOnFocus = true;
				fixWireHud.Visible = isShow;
				menuCursor.isWiring = true;

			} else if (Input.IsActionJustPressed("ui_cancel") && isShow == true)
			{
				isShow = false;
				player.inputOnFocus = false;
				fixWireHud.Visible = isShow;
			}
		}
	}

	public void ShowText()
	{
		if (interact == true)
		{
			player.to_label.Text = "E to interact";
			player.ui_animations.Play("appear");
		}
		else if (interact == false)
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
		isShow = false;
		player.inputOnFocus = false;
		fixWireHud.Visible = isShow;
		GD.Print("Cable Reparer");
	}

}