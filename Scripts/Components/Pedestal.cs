using Godot;
using System;

public partial class Pedestal : Node
{
	// Meme script que objectToFix donc les réunir ? 
	private int PedestalsActives = 0;
	private int PedestalsToActive = 0;

	public override void _Ready()
	{
		foreach (Node i in GetNode<Node>("AreasPedestal").GetChildren())
		{
			i.Connect("ON",new Callable(this,"PedestalActivated"));
			i.Connect("OFF",new Callable(this,"PedestalDesactivated"));
			PedestalsToActive++;
		}
	}

	public override void _Process(double delta)
	{
	}


	private void PedestalActivated(ObjectFixArea ObjectFixedArea, Node2D body) 
	{
		if(body.IsInGroup("Pedestal"))
		{
			ObjectFixedArea.Call("FixedChange");
			PedestalsActives++;
			
			if (PedestalsActives >= PedestalsToActive){
				GD.Print("Pedestal activated");
				// Changer la variable dans le global ou faire un emit au main
			}
		}
	}

	private void PedestalDesactivated(ObjectFixArea Area, Node2D body) 
	{
		if(body.IsInGroup("Pedestal"))
		{
			Area.Call("FixedChange");
			GD.Print();
			PedestalsActives--;
		}
	}
}
