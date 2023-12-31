using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node2D
{

	private Scene_transition SceneTransition = null;
	private Global global = null;
	// L'error "audio_device_init" est normale car y a pas de carte son
	// Pareil pour "init : WASAPI: init_output_device error


	private Trashcan trashcan = null;
	private ObjectForFix objectForFix = null;
	private ColorCode coloredpc = null;
	private AllPc allpc = null;
	private Pedestal pedestal = null;
	private ObjectForFixVert fixvert = null;
	private Bedroom bedroom = null;
	private FruitGame fruitgame = null;

	//private Camera2D camera_player = null;

	private Control pauseMenu;

	public bool paused = false;

	public override void _Ready()
	{
		GetNode("Player").GetNode<Camera2D>("Camera2D").MakeCurrent();
		coloredpc = GetNode<ColorCode>("ColoredPc");
		objectForFix = GetNode<ObjectForFix>("ObjectForFix");
		trashcan = GetNode<Node>("Trashcan_fix").GetNode<Trashcan>("Trashcan");
		allpc = GetNode<AllPc>("AllPc");
		pedestal = GetNode<Pedestal>("Pedestal");
		fixvert = GetNode<ObjectForFixVert>("ObjectForFixVert");
		bedroom = GetNode<Bedroom>("Bedroom");
		pauseMenu = GetNode<Control>("PauseCanvas/Pause");
		fruitgame = GetNode<FruitGame>("FruitGame");

		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		global = GetParent().GetNode<Global>("Global");
		foreach (Node i in GetNode<Node>("TP_all").GetChildren())
		{
			i.Connect("script_changed",new Callable(this,"Tp_entered"));
		}
		List<bool> LevelBool = new List<bool>
		{
			allpc.STATE,
			coloredpc.STATE,
			objectForFix.STATE,
			trashcan.Full,
			pedestal.STATE,
			fruitgame.STATE,
			fixvert.STATE,
			bedroom.STATE
		};
		global.List_Level_Bools = LevelBool;
			
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Pause") && !GetNode<Player>("Player").inputOnFocus)
		{
			PauseMenu();
		}
	}

	public void Tp_entered(string _scenePath, Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			Player temp = (Player)body;
			temp.Speed = 0;
			// Mettre tout ça dans une node et checker chaque enfant ptet (ptet pas possible vu que faut cast du bon type, ou tous meme class pour le state)
			
			List<string> LevelString = new List<string>
			{
				"Some people don't turn off their pc...",
				"Colors means something together... Right ?",
				"Hmmm... Sometimes things aren't were they're supposed to be.",
				"Detritus on the floor... Not the best for the company.",
				"Someone left some crates somewhere...",
				"Number of caracters means something ?",
				"2 and 3 on the other side ? Hmmm...",
				"Everyone wants their favorite item in their bedroom..."
			};
			List<bool> LevelBool = new List<bool>
			{
				allpc.STATE,
				coloredpc.STATE,
				objectForFix.STATE,
				trashcan.Full,
				pedestal.STATE,
				fruitgame.STATE,
				fixvert.STATE,
				bedroom.STATE
			};
			GD.Print("all pc : "+allpc.STATE);
			GD.Print("colored pc : "+coloredpc.STATE);
			GD.Print("objectforfix : "+objectForFix.STATE);
			GD.Print("trashcan : "+trashcan.Full);
			GD.Print("pedestal : "+pedestal.STATE);
			GD.Print("fruitgame : "+fruitgame.STATE);
			GD.Print("fixvert : "+fixvert.STATE);
			GD.Print("bedroom : "+bedroom.STATE);

			global.List_Level_Bools = LevelBool;
			global.List_Level_Texts = LevelString;
			global.TpEnd = "Scenes/Menu/Credits_normal.tscn";
			global.TpOut = "main.tscn";
			
			
			SceneTransition.Call("changeScene",_scenePath,false); 
		}
		else {
			GD.Print("Non_player collision");
		}
	}

	public void PauseMenu()
	{
		Pause tempPause = (Pause)pauseMenu;
		if (paused)
		{
			pauseMenu.Hide();
			Engine.TimeScale = 1;
		}
		else 
		{
			pauseMenu.Show();
			Engine.TimeScale = 0;
		}
		paused = !paused;
		tempPause.UpdatePause();
	}
}


