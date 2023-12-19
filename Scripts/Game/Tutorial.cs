using Godot;
using System;
using System.Collections.Generic;

public partial class Tutorial : Node2D
{
	private ObjectForFix FixPattern = null;
	private Global global = null;
	private Scene_transition SceneTransition = null;
	public override void _Ready()
	{
		FixPattern = GetNode<ObjectForFix>("FixPattern");
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		global = GetParent().GetNode<Global>("Global");
		foreach (Node i in GetNode<Node>("Tps").GetChildren())
		{
			i.Connect("script_changed",new Callable(this,"Tp_entered"));
		}
	}

	private void Tp_entered(string _scenePath, Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			Player temp = (Player)body;
			temp.Speed = 0;

			global.List_Level_Bools.Add(FixPattern.STATE);
			global.List_Level_Texts.Add("A chair not around a table ?");
			global.Tp = "Scenes/Menu/Menu.tscn";
			SceneTransition.Call("changeScene",_scenePath,false);
		}
	}

}
