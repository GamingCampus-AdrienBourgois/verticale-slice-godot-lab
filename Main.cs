using Godot;
using System;
// Cool
public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello");
		foreach (Node i in GetNode<Node>("TP_all").GetChildren())
		{
			i.Connect("Player_entered",new Callable(this,"_on_tp_body_entered"));
			GD.Print("tp ready");
			// GOOOD
			
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_tp_body_entered(Node2D body)
	{
		if (body.IsInGroup("Player")){
			// a
			//GetTree().ChangeSceneToFile("main.tscn"); // Pas d'autres scenes pour l'instant
		}
	}


	
}


