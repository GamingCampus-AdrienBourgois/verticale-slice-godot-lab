using Godot;
using System;
// Cool
public partial class Main : Node2D
{

	private Scene_transition SceneTransition = null;
	private Global global = null;
	// L'error "audio_device_init" est normale car y a pas de carte son
	// Pareil pour "init : WASAPI: init_output_device error

	// Called when the node enters the scene tree for the first time.

	private Trashcan trashcan = null;
	private ObjectForFix objectForFix = null;
	private ColorCode coloredpc = null;


	public override void _Ready()
	{
		coloredpc = GetNode<ColorCode>("ColoredPc");
		objectForFix = GetNode<ObjectForFix>("ObjectForFix");
		trashcan = GetNode<Node>("Trashcan_fix").GetNode<Trashcan>("Trashcan");

		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		global = GetParent().GetNode<Global>("Global");
		foreach (Node i in GetNode<Node>("TP_all").GetChildren())
		{
			i.Connect("script_changed",new Callable(this,"Tp_entered"));
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void Tp_entered(string _scenePath, Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			global.Niveau_1 = true;
			//if(coloredpc.STATE == true){ global.Niveau_2 = true; }
			//if(objectForFix.STATE == true){ global.Niveau_3 = true; }
			//if(trashcan.Full == true){ global.Niveau_4 = true; }
			global.Niveau_2 = true;
			global.Niveau_3 = true;
			global.Niveau_4 = true;
			SceneTransition.Call("changeScene",_scenePath); 
		}
		else {
			GD.Print("Non_player collision");
		}
	}
}


