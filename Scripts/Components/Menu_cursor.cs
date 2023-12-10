using Godot;
using System;

public partial class Menu_cursor : TextureRect
{
	[Export]
	public NodePath nodePath;
	[Export]
	public Vector2 cursorOffset;

	private Fix_wire_hud fixWireHud;
	private GridContainer menu_parent;

	private Line2D line;

	private int cursorIndex = 0;
	private bool isWireSelected = false;
	private bool colorMatched;
	private Vector2 wireSelected = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		menu_parent = GetNode(nodePath) as GridContainer;
		fixWireHud = (Fix_wire_hud)GetParent();

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 input = Vector2.Zero;

		if (Input.IsActionJustPressed("ui_up"))
		{
			input.Y -= 1;
		}
		if (Input.IsActionJustPressed("ui_down"))
		{
			input.Y += 1;
		}

		if (menu_parent is GridContainer)
		{
			SetCursorFromIndex(cursorIndex + (int)input.Y * menu_parent.Columns);
		}

		if (Input.IsActionJustPressed("ui_accept") && isWireSelected == false) 
		{
			wireSelected.X = cursorIndex;
			SetCursorFromIndex(cursorIndex + 1);
			isWireSelected = true;

		}
		else if (Input.IsActionJustPressed("ui_accept") && isWireSelected == true)
		{
			wireSelected.Y = cursorIndex;
			colorMatched = CheckWiresColor(wireSelected);

		}

		if (colorMatched == true)
		{
			SetNewLine(wireSelected);
		}
	}

	public void SetNewLine(Vector2 wire)
	{
		ColorRect ItemLeft = GetMenuItemAtIndex((int)wire.X) as ColorRect;
		ColorRect ItemRight = GetMenuItemAtIndex((int)wire.Y) as ColorRect;

		Godot.Vector2 startPos = ItemLeft.GlobalPosition;
		Vector2 endPos = ItemRight.GlobalPosition;

		line = new Line2D();

		line.Points = startPos;
		line.Points = endPos;
		line.Width = 2.0f;
		line.DefaultColor = ItemLeft.Color;

		GetParent().AddChild(line);
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

	private bool CheckWiresColor(Vector2 wire) 
	{
		var itemLeft = GetMenuItemAtIndex((int)wire.X) as ColorRect;
		var itemRight = GetMenuItemAtIndex((int)wire.Y) as ColorRect;

		if (itemLeft == null || itemRight == null)
		{
			return false;
		}

		if (itemLeft.Color == itemRight.Color) 
		{
			return true;
		}
		else
		{
			return false;
		}

	}
	
}
