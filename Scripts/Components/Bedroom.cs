using Godot;
using System;

public partial class Bedroom : Node
{
	private int Bedrooms = 0;
	private int BedroomsOK = 0;

	public bool STATE = false;
	public override void _Ready()
	{
		foreach (Node i in GetNode<Node>("Areas").GetChildren())
		{
			i.Connect("ON",new Callable(this,"BedActivated"));
			i.Connect("OFF",new Callable(this,"BedDesactivated"));
			Bedrooms++;
		}
	}

	private void BedActivated(ObjectFixArea area, Node2D body)
	{
		if(body.IsInGroup(area.Call("GetGroup").ToString()))
		{
			GD.Print("Bon item !");
			area.Call("FixedChange");
			GD.Print("body :"+body.Name);
			BedroomsOK++;
			if(BedroomsOK >= Bedrooms)
			{
				STATE = true;
				GD.Print("FINISHED");
			}
		}
	}
	private void BedDesactivated(Area2D area, Node2D body)
	{
		if(body.IsInGroup(area.Call("GetGroup").ToString()))
		{
			area.Call("FixedChange");
			BedroomsOK--;
			STATE = false;
		}
	}


}
