using Godot;
using System;

public partial class Escape_the_game : Control
{
	private Scene_transition SceneTransition = null;
	//private Global global = null;
	[Export]
	int height = 5;
	[Export]
	int width = 8;
	[Export]
	int ScoreToHave = 3;
	[Export]
	float TimeAll = 10;

	Vector2 selected = new Vector2(0,0);

	int EscapeSolved = 0;

	Node vbox = null;
	Label label = null;
	Timer TimerAll = null;
	public override void _Ready()
	{
		AddChild(new Timer
		{
			Name = "TimerAll",
			Autostart = true,
			WaitTime = TimeAll,
			OneShot = true
		});
		TimerAll = GetNode<Timer>("TimerAll");
		TimerAll.Connect(Timer.SignalName.Timeout,new Callable(this,"TimeRanOut"));
		// i.Connect("script_changed",new Callable(this,"Tp_entered"));
		//global = GetParent().GetNode<Global>("Global");
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		
		label = GetNode<Label>("Label");
		
		// AddChild(new Label
		// {
		// 	LayoutMode = 1,
		// 	AnchorsPreset = 5,
		// 	Theme
		// });
		
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
		// Faire un algo qui check si il est faisable, si l'est pas, il le refait ou enlève un carré
		label.Text = "Solved: "+EscapeSolved;
		Vector2 exit = new Vector2(0,0);
		var y = 0;
		foreach(Node x in vbox.GetChildren())
		{
			for (int i = 0; i < width; i++)
			{
				ColorRect rectangle = x.GetChild<ColorRect>(i);
				Random rnd = new Random();
				new Godot.Vector2(rnd.Next(0,height-1),rnd.Next(0,width-1));
				rectangle.Color = Colors.White;
				if(rnd.Next(1,10) > 7){
					rectangle.Color = Colors.Red;
				}
				if(rnd.Next(1,10) > 9 && exit == new Vector2(0,0))
				{
					rectangle.Color = Colors.Green;
					exit = new Vector2(y,i);
				}
				if(i == width-1 && exit == new Vector2(0,0) && vbox.GetChild(height-1) == x)
				{
					rectangle.Color = Colors.Green;
					exit = new Vector2(y,i);
				}
				else if(i == 0 && vbox.GetChild(0) == x && y != exit.Y && i != exit.X)
				{
					rectangle.Color = Colors.White;
				}
			}
			y++;
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

	private void TimeRanOut()
	{
		SceneTransition.Call("changeScene",this,true);
	}
}
