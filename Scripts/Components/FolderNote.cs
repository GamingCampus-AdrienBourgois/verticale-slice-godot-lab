using Godot;
using System;

public partial class FolderNote : CharacterBody2D
{

	private Player player;
	private CanvasLayer FolderHud;
	private bool interact = false;
	private bool isShow = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent().GetParent().GetNode<Player>("Player");
		FolderHud = GetNode<CanvasLayer>("CanvasLayer");
		if (FolderHud != null)
		{
			FolderHud.Visible = isShow;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			CloseFolder();
		}

	}

	public void InteractFolder() 
	{
		if (!isShow)
		{
			isShow = true;
			FolderHud.Visible = isShow;
		}
		else {
			CloseFolder();
		}
	}
	public void CloseFolder()
	{
		isShow = false;
		FolderHud.Visible = isShow;
	}
}
