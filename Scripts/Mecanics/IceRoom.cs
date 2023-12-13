using Godot;
using System;

public partial class IceRoom : Node
{
	Node2D player = null;

	private void _on_player_detect_on(Area2D This, Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			player = body;
		}
	}
	private void _on_ice_dec_1_on(Area2D This, Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			GD.Print("Entered");
			var temp = (Player)player;
			temp.OnIce = true;
		}	
	}
	
	private void _on_ice_dec_1_off(Area2D This, Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			GD.Print("Exited");
			var temp = (Player)player;
			temp.OnIce = false;
		}
	}

}


