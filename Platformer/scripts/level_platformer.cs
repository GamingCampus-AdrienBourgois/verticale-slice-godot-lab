using Godot;
using System;

public partial class level_platformer : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (Node i in GetNode<Node>("TP_all").GetChildren())
		{
			i.Connect("script_changed",new Callable(this,"Tp_entered"));
			GD.Print("tp ready");
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
			GD.Print("Player !");
			GetTree().ChangeSceneToFile(_scenePath);
		}
		else {
			GD.Print("Non_player collision");
		}
	}
}
