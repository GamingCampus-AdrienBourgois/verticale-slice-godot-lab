using Godot;
using System;

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

	public override void _Ready()
	{
		coloredpc = GetNode<ColorCode>("ColoredPc");
		objectForFix = GetNode<ObjectForFix>("ObjectForFix");
		trashcan = GetNode<Node>("Trashcan_fix").GetNode<Trashcan>("Trashcan");
		allpc = GetNode<AllPc>("AllPc");
		pedestal = GetNode<Pedestal>("Pedestal");
		fixvert = GetNode<ObjectForFixVert>("ObjectForfixVert");
		

		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		global = GetParent().GetNode<Global>("Global");
		foreach (Node i in GetNode<Node>("TP_all").GetChildren())
		{
			i.Connect("script_changed",new Callable(this,"Tp_entered"));
		}
	}

	public override void _Process(double delta)
	{
	}
	
	public void Tp_entered(string _scenePath, Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			Player temp = (Player)body;
			temp.Speed = 0;

			// Mettre tout ça dans une node et checker chaque enfant ptet (ptet pas possible vu que faut cast du bon type, ou tous meme class pour le state)
			if(allpc.STATE == true){ global.Niveau_1 = true; }
			if(coloredpc.STATE == true){ global.Niveau_2 = true; }
			if(objectForFix.STATE == true){ global.Niveau_3 = true; }
			if(trashcan.Full == true){ global.Niveau_4 = true; }
			if(pedestal.STATE == true) { global.Niveau_5 = true; }
			if(fixvert.STATE == true) { global.Niveau_6 = true; }
			
			SceneTransition.Call("changeScene",_scenePath); 
		}
		else {
			GD.Print("Non_player collision");
		}
	}
}


