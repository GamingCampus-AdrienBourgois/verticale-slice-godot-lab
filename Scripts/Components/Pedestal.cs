using Godot;
using System;

public partial class Pedestal : Node
{

	public override void _Ready()
	{
		foreach (Node i in GetNode<Node>("AreasPedestal").GetChildren())
		{
			i.Connect("ObjectFixed",new Callable(this,"PedestalActivated"));
		}
	}

	public override void _Process(double delta)
	{
	}

	// Faire aussi si ça quitte la plaque de pression
	private void PedestalActivated(ObjectFixArea ObjectFixedArea, Node2D body) 
	{
		if(body.IsInGroup("Pedestal"))
		{
			
		}
	}
}
