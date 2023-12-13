using Godot;
using System;

public partial class Escape_the_game : Control
{
	private Scene_transition SceneTransition = null;
	//private Global global = null;
	int height = 5;
	int width = 8;

	Vector2 selected = new Vector2(0,0);

	int EscapeSolved = 0;

	Node vbox = null;
	Label label = null;
	public override void _Ready()
	{
		//global = GetParent().GetNode<Global>("Global");
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		
		label = GetNode<Label>("Label");
		
		InitMap();
		CreateLab();
	}
	
	public override void _Input(InputEvent @event)
	{
		Vector2 AncientSelec = selected;
		// Mettre que si il appuie sur une touche ça enlève le rouge
		if(@event.IsActionPressed("Up")){selected.X -= 1;}
		else if(@event.IsActionPressed("Down")){selected.X += 1;}
		else if(@event.IsActionPressed("Right")){selected.Y += 1;}
		else if(@event.IsActionPressed("Left")){selected.Y -= 1;}
		if(selected.X < 0 || selected.Y < 0 || selected.X > height-1 || selected.Y > width-1)
		{
			selected = AncientSelec;
		}
		ToColor(AncientSelec,Colors.White);
		ToColor(selected,Colors.Blue);
	}
	//SceneTransition.Call("changeScene",_scenePath); si il foire et va sur une case rouge
	
	private void ToColor(Godot.Vector2 thing, Color color)
	{
		var hbox_to = vbox.GetChild((int)thing.X);
		var childHbox = hbox_to.GetChild((int)thing.Y);
		ColorRect temp = (ColorRect)childHbox;
		if(temp.Color == Colors.Red)
		{
			// SceneTransition.Call("changeScene",this);
			CreateLab();
		}
		else if(temp.Color == Colors.Green)
		{
			EscapeSolved++;
			CreateLab();
		}
		else if (temp.Color != Colors.Green && temp.Color != Colors.Red)
		{
			temp.Color = color;
		}
	}

	private void CreateLab()
	{
		label.Text = "Solved: "+EscapeSolved;
		bool exit = false;
		foreach(Node x in vbox.GetChildren())
		{
			for (int i = 0; i < width; i++)
			{
				ColorRect rectangle = x.GetChild<ColorRect>(i);
				Random rnd = new Random();
				new Godot.Vector2(rnd.Next(0,height-1),rnd.Next(0,width-1));
				rectangle.Color = Colors.White;
				if(rnd.Next(1,10) > 8){
					rectangle.Color = Colors.Red;
				}
				if(rnd.Next(1,10) > 9 && exit == false)
				{
					rectangle.Color = Colors.Green;
				}
				if(i == width-1 && exit == false && vbox.GetChild(vbox.GetChildCount()-1) == x)
				{
					rectangle.Color = Colors.Green;
				}
				if(i == 0 && vbox.GetChild(0) == x)
				{
					rectangle.Color = Colors.White;
				}

			}
		}
		selected.X = 0;
		selected.Y = 0;
		ToColor(selected,Colors.Blue);
	}

	private void InitMap()
	{
		AddChild(new ColorRect
		{
			Color = Colors.Black,
			LayoutMode = 1,
			AnchorsPreset = 15
		});

		AddChild(new VBoxContainer
		{
			Name = "Vbox",
			LayoutMode = 1,
			AnchorsPreset = 8
		});
		vbox = GetNode("Vbox");

		for (int i = 0; i < height; i++)
		{
			var Hbox = new HBoxContainer();
			vbox.AddChild(Hbox);
		}
		foreach(Node x in vbox.GetChildren())
		{
			for (int i = 0; i < width; i++)
			{
				var colorRect = new ColorRect
				{
					CustomMinimumSize = new Godot.Vector2(50, 50)
				};
				x.AddChild(colorRect);
			}
		}
	}


}
