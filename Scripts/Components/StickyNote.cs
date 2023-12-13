using Godot;
using System;

public partial class StickyNote : Area2D
{

	private Player player;
	private CanvasLayer stickyNoteHud;

	private bool interact = false;
	private bool isShow = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent().GetParent().GetNode<Player>("Player");
		stickyNoteHud = GetNode<CanvasLayer>("CanvasLayer");
		if (stickyNoteHud != null)
		{
			stickyNoteHud.Visible = isShow;
		}
	}	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(interact)
		{
			if (Input.IsActionJustPressed("Interact") && !isShow)
			{
				ShowStickyNote();
			}
			else if (Input.IsActionJustPressed("ui_cancel") && isShow)
			{
				ShowStickyNote();
			}
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

	public void _on_body_entered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			interact = true;
			ShowText();
		}
	}

	public void _on_body_exited(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			interact = false;
			ShowText();
		}
	}

	private void ShowStickyNote()
	{
		if (!isShow)
		{
			isShow = true;
			player.inputOnFocus = true;
			stickyNoteHud.Visible = isShow;
		}
		else if (isShow)
		{
			isShow = false;
			player.inputOnFocus = false;
			stickyNoteHud.Visible = isShow;
		}
	}
}
