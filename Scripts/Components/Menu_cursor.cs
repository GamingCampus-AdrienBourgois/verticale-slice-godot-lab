using Godot;
using System;

public partial class Menu_cursor : TextureRect
{
	[Export]
	public NodePath nodePath;
	[Export]
	public Vector2 cursorOffset;

	private Fix_wire_hud fixWireHud;

	private Line2D line;

	private int cursorIndex = 0;
	private bool isWireSelected = false;
	private bool colorMatched;
	private Vector2 wireSelected = Vector2.Zero;
	GridContainer menu_parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		menu_parent = GetNode(nodePath) as GridContainer;
		fixWireHud = (Fix_wire_hud)GetParent();

		CallDeferred("DeferredInitialization");
	}

	private void DeferredInitialization()
	{
    	// Le reste de votre initialisation...
    	line = new Line2D();
    	GetParent().AddChild(line);

    	line.DefaultColor = new Color(1, 1, 1);
    	line.Width = 2.0f;
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

			UpdateLine();
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

	private void UpdateLine() 
	{
		var startMenuItem = GetMenuItemAtIndex((int)wireSelected.X);
    	var endMenuItem = GetMenuItemAtIndex((int)wireSelected.Y);

    	if (startMenuItem == null || endMenuItem == null)
    	{
        	return;
    	}

		var startPos = -startMenuItem.GlobalPosition + startMenuItem.Size / 2.0f;
    	var endPos = -endMenuItem.GlobalPosition + endMenuItem.Size / 2.0f;
		GD.Print(startMenuItem.GlobalPosition + " " + endMenuItem.GlobalPosition);

		// Effacez tous les points actuels de la ligne
    	line.ClearPoints();

    	// Ajoutez les deux points à la ligne
    	line.AddPoint(startPos);
    	line.AddPoint(endPos);
	}
}
