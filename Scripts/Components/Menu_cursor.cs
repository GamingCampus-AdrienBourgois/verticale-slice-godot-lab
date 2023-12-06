using Godot;
using System;

public partial class Menu_cursor : TextureRect
{
	[Export]
    public NodePath nodePath;
	[Export]
	public Vector2 cursorOffset;

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

		if (menu_item != null)
		{
			return;
		}

		var position = menu_item.RectGlobalPosition;
		var size = menu_item.RectSize;

		RectGlobalPosition = new Vector2(position.x, position.y - size.y / 2.0) - (RectSize/2.0) - cursorOffset;

    }
}
