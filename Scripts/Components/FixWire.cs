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

	[Export]
	string animOpen = null;
	[Export]
	string animClosed = null;

	AnimatedSprite2D sprite = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite.Play(animClosed);
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
			player.inputOnFocus = isShow;
			fixWireHud.Visible = isShow;
			menuCursor.isWiring = true;
		}
		else
		{
			isShow = false;
			player.inputOnFocus = isShow;
			fixWireHud.Visible = isShow;
		}
	}

	public void ShowText()
	{
		player.to_label.Text = "E to interact";
		player.ui_animations.Play("appear");
		
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
			HideText();
		}

	}

	private void HideText()
	{
		player.ui_animations.PlayBackwards("appear");
	}

	public void WireIsFix()
	{
		ShowWireHud();
		HideText();
		interact = false;
		GD.Print("Cable Reparer");
		// Faire plus propre avec un signal si on veut
		Node WireDoors = GetParent().GetNode("Doors").GetNode("WireDoors");
		sprite.Play(animOpen);
		foreach(door i in WireDoors.GetChildren())
		{
			i.DoorOpenedChange();
		}
	}
}
