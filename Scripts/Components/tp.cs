using Godot;
using System;

public partial class tp : Area2D
{
	[Export] 
	private string _scenePath;
	
	//[Signal]
	//public delegate void Player_enteredEventHandler();

	public override void _Ready()
	{

	}
	
	
	public string GetScenePath() {
		return _scenePath;
	}
	private void _on_body_entered(Node2D body)
	{
		GD.Print("pb ?");
		EmitSignal("script_changed",_scenePath,body);
	}
}


