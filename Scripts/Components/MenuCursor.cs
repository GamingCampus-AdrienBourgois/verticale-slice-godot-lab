using Godot;
using System;

public partial class MenuCursor : TextureRect
{
	[Export]
	public NodePath nodePath;
	[Export]
	public Vector2 cursorOffset;

	private FixWire fixWire;
	private CanvasLayer fixWireHud;
	private GridContainer menu_parent;

	private Line2D line;

	private int cursorIndex = 0;
	private int WireFixNb = 0;
	private bool isWireSelected = false;
	private bool colorMatched;
	public bool isWiring = false;
	private bool[] isWireAlreadyFix = new bool[7];
	private Vector2 wireSelected = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		menu_parent = GetNode(nodePath) as GridContainer;
		fixWireHud = (CanvasLayer)GetParent();
		fixWire = GetTree().Root.GetNode("Main").GetNode("FixWire") as FixWire;

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

		if(isWiring)
		{
			if (Input.IsActionJustPressed("ui_accept") && !isWireSelected) 
			{
				wireSelected.X = cursorIndex;
				SetCursorFromIndex(cursorIndex + 1);
				isWireSelected = true;

			}
			else if (Input.IsActionJustPressed("ui_accept") && isWireSelected)
			{
				wireSelected.Y = cursorIndex;
				colorMatched = CheckWiresColor(wireSelected);
				if (colorMatched)
				{
					SetWireFix((int)wireSelected.X);
				}
				else if (!colorMatched)
				{
					GD.Print("Mauvais branchement");
					SetCursorFromIndex(cursorIndex - 1);
					isWireSelected = false;
				}
			}
		}
		
		
	}

	private void SetWireFix(int index) 
	{

		if (!isWireAlreadyFix[index])
		{
			isWireAlreadyFix[index] = true;
			SetCursorFromIndex(cursorIndex - 1);
			isWireSelected = false;
			WireFixNb++;
			GD.Print("Cable brancher"); 
			if (WireFixNb >= 4)
			{
				isWiring = !isWiring;
				fixWire.WireIsFinish = true;
				fixWire.WireIsFix();
			}
		}
		else if (isWireAlreadyFix[index])
		{
			GD.Print("Cable deja brancher");
			SetCursorFromIndex(cursorIndex - 1);
			isWireSelected = false;
		}
	}

	// public void SetNewLine(Vector2 wire)
	// {
	// 	ColorRect ItemLeft = GetMenuItemAtIndex((int)wire.X) as ColorRect;
	// 	ColorRect ItemRight = GetMenuItemAtIndex((int)wire.Y) as ColorRect;

	// 	Godot.Vector2 startPos = ItemLeft.GlobalPosition;
	// 	Vector2 endPos = ItemRight.GlobalPosition;

	// 	line = new Line2D();

	// 	line.Points = startPos;
	// 	line.Points = endPos;
	// 	line.Width = 2.0f;
	// 	line.DefaultColor = ItemLeft.Color;

	// 	GetParent().AddChild(line);
	// }

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
			itemLeft.SelfModulate = Color.FromHsv(0.0f,0.0f,0.0f,1.0f);
			itemRight.SelfModulate = Color.FromHsv(0.0f,0.0f,0.0f,1.0f);
			return true;
		}
		else
		{
			return false;
		}

	}
	
}
