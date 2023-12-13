using Godot;
using System;

public partial class level_platformer : Node
{
	private Scene_transition SceneTransition = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		foreach (Node i in GetNode<Node>("TP_all").GetChildren())
		{
			i.Connect("script_changed",new Callable(this,"Tp_entered"));
		}
	}

	// Faire un poutit script pour faire varier la speed du player

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void Tp_entered(string _scenePath, Node2D body)
	{
		if (body.IsInGroup("Player")){
			SceneTransition.Call("changeScene","main.tscn",false);
		}
		else {
			GD.Print("Non_player collision");
			
		}
	}
}
