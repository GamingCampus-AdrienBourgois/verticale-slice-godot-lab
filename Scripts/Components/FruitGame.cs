using Godot;
using System;
using System.Data;

public partial class FruitGame : Node
{
	[Export]
	string BodyGroup = "Durian";
	public bool STATE = false;
	public override void _Ready()
	{
		foreach (Node i in GetNode<Node>("Areas").GetChildren())
		{
			i.Connect("ON",new Callable(this,"FruitON"));
			i.Connect("OFF",new Callable(this,"FruitOFF"));
		}
	}

	private void FruitON(ObjectFixArea Area, Node2D body)
	{
		if(Area.IsInGroup("True"))
		{
			if(body.IsInGroup(BodyGroup))
			{
				GD.Print("Porte active");
				// Fait la porte
				Node IceDoor = GetParent().GetNode("Doors").GetNode("IceDoor");
				foreach (door i in IceDoor.GetChildren())
				{
					// i.DoorOpenedChange();
					i.CallDeferred("DoorOpenedChange");
				}
				if(body.IsInGroup("True"))
				{
					GD.Print("Valid");
					STATE = true;
				}
			}
		}
	}

	private void FruitOFF(ObjectFixArea Area, Node2D body)
	{
		if(Area.IsInGroup("True"))
		{
			if(body.IsInGroup(BodyGroup))
			{
				GD.Print("Porte desactivé");
				// Fait la porte
				Node IceDoor = GetParent().GetNode("Doors").GetNode("IceDoor");
				foreach (door i in IceDoor.GetChildren())
				{
					i.CallDeferred("DoorOpenedChange");
				}
				if(body.IsInGroup("True"))
				{
					GD.Print("not valid");
					STATE = false;
				}
			}
		}

	}


}
