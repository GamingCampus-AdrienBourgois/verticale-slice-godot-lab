using Godot;
using System;

public partial class Menu_cursor : TextureRect
{
	[Export]
	public NodePath nodePath;
	[Export]
	public Vector2 cursorOffset;

	private CanvasLayer fixWireHud;

	int cursorIndex = 0;
	Node menu_parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		menu_parent = GetNode(nodePath) as Node;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 input = Vector2.Zero;

		if (Input.IsActionJustPressed("ui_up"))
		{
			GD.Print("Poutre");
			input.Y -= 1;
		}
		if (Input.IsActionJustPressed("ui_down"))
		{
			input.Y += 1;
		}

		if (menu_parent is VBoxContainer)
		{
			SetCursorFromIndex(cursorIndex + (int)input.Y);
			GD.Print(cursorIndex);
		}
	}

	public Control GetMenuItemAtIndex(int index)
	{
		if (menu_parent == null) {
			return null;
		}

		if (index >= menu_parent.GetChildCount() || index < 0)
		{
			return null;
		}

		return menu_parent.GetChild(index) as Control;

	}

	public void SetCursorFromIndex(int index)
	{
		var menu_item = GetMenuItemAtIndex(index);

		if (menu_item == null)
		{
			return;
		}

		Vector2 Position = menu_item.GlobalPosition;
		Vector2 Size = menu_item.Size;

		GlobalPosition = new Vector2(Position.X, (Position.Y - Size.Y / 2.0f)) - (Size / 2.0f) - cursorOffset;

		cursorIndex = index;

	}
}
