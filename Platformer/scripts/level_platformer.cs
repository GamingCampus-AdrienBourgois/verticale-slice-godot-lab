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
		// Juste si echap enlève
	}
	public void Tp_entered(string _scenePath, Node2D body)
	{
		if (body.IsInGroup("Player")){
			SceneTransition.Call("changeScene",_scenePath,false);
		}
		else {
			GD.Print("Non_player collision");
			
		}
	}

	// Flemme de faire une classe checkpoint c'est juste pour 1
	private void _on_check_the_point_body_entered(Node2D body)
	{
		GetNode<Node2D>("Checkpoint").GlobalPosition = GetNode<Node2D>("CheckThePoint").GlobalPosition;
		GetNode<Node2D>("CheckThePoint").QueueFree();
	}

}


