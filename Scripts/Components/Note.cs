using Godot;
using System;

public partial class Note : CharacterBody2D
{
	[Export]
	public bool StickyNote = false;
	[Export]
	public Texture2D img;
	[Export]
	public bool isPickable = false;
	[Export]
	public string text;

	private CanvasLayer FolderNoteHud;
	private CanvasLayer StickyNoteHud;
	private Player player;
	private Label txt_label;

	private bool isShow = false;
	private bool interact = false;

	public override void _Ready()
	{
		FolderNoteHud = GetNode<CanvasLayer>("FolderNote");
		StickyNoteHud = GetNode<CanvasLayer>("StickyNote");
		player = GetParent().GetParent().GetNode<Player>("Player");
		txt_label = StickyNoteHud.GetNode<ColorRect>("ColorRect").GetNode<Label>("Label");

		if (FolderNoteHud != null )
		{
			FolderNoteHud.Visible = isShow;
		}

		if (StickyNoteHud != null)
		{
			StickyNoteHud.Visible = isShow;
		}

		if (isPickable)
		{
			AddToGroup("Pickable");
		}

		if (!StickyNote)
		{
			AddToGroup("FolderNote");

			GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;


			if (img != null)
			{
				FolderNoteHud.GetNode<PanelContainer>("PanelContainer").GetNode<Sprite2D>("Sprite2D").Texture = img;
			}
		}
		else
		{
			if(text != null)
			{
				txt_label.Text = text;
			}
			else
			{
				txt_label.Text = "Bla Bla";
			}
			
		}
	}

	public override void _Process(double delta)
	{
		if (!StickyNote)
		{
			if (Input.IsActionJustPressed("ui_cancel"))
			{
				CloseFolder();
			}

		}
		else
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
	}

	public void CloseFolder()
	{
		isShow = false;
		FolderNoteHud.Visible = isShow;
	}

	public void InteractFolder()
	{
		if(!isShow)
		{
			isShow = true;
			FolderNoteHud.Visible = isShow;
		}
		else
		{
			CloseFolder();
		}
	}

	public void _on_area_2d_body_entered(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			interact = true;
			ShowText();
		}
	}

	public void _on_area_2d_body_exited(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			interact = false;
			ShowText();
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

	private void ShowStickyNote()
	{
		if (!isShow)
		{
			isShow = true;
			player.inputOnFocus = true;
			StickyNoteHud.Visible = isShow;
		}
		else if (isShow)
		{
			isShow = false;
			player.inputOnFocus = false;
			StickyNoteHud.Visible = isShow;
		}
	}

}
