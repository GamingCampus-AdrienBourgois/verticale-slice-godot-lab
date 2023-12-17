using Godot;
using System;

public partial class NotTp_TP : Area2D
{
	Escape_the_game game_escape = null;
	CanvasLayer game_escape_hud = null;
	CanvasLayer pattern_game_hud = null;
	Pattern_game pattern_game = null;

	Player player = null;
	bool already = false;
	float AncientSpeed;
	public override void _Ready()
	{
		player = GetParent().GetParent().GetNode<Player>("Player");
		Connect(Area2D.SignalName.BodyEntered,new Callable(this,"OnBody"));
		game_escape = GetParent().GetNode("Game_escape").GetNode<Escape_the_game>("EscapeTheGame");
		game_escape_hud = GetParent().GetNode<CanvasLayer>("Game_escape");
		pattern_game_hud = GetParent().GetNode<CanvasLayer>("PatternGame");
		pattern_game = pattern_game_hud.GetNode<Pattern_game>("Pattern_game");
	}
	private void OnBody(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			if(Name == "In")
			{
				//game_escape_hud.Visible = true;
				pattern_game_hud.Visible = true;
				player.inputOnFocus = true;
				//game_escape.Connect("Escaped",new Callable(this,"OnEscaped"));
				//game_escape.Start();
				pattern_game.Connect("Pattern",new Callable(this,"OnPattern"));
				pattern_game.Start();
				
				GD.Print("Hello");
				already = true;
			}
			if(Name == "Out")
			{
				body.GlobalPosition = GetParent().GetNode("In").GetNode<Node2D>("Marker2D").GlobalPosition;
			}
		}
	}
	
	private void OnEscaped()
	{
		player.GlobalPosition = GetParent().GetNode("Out").GetNode<Node2D>("Marker2D").GlobalPosition;
		game_escape_hud.QueueFree();
		player.inputOnFocus = false;
	}
	private void OnPattern()
	{
		player.GlobalPosition = GetParent().GetNode("Out").GetNode<Node2D>("Marker2D").GlobalPosition;
		pattern_game_hud.QueueFree();
		player.inputOnFocus = false;
	}
}
