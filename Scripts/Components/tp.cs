using Godot;
using System;

public partial class tp : Area2D
{
	[Export] 
	private string _scenePath;

	private Signal Player_entered;
	
	public string GetScenePath() {
		return _scenePath;
	}
	private void _on_body_entered(Node2D body)
	{
		GD.Print("Hello");
		EmitSignal("Player_entered");
	}
}


