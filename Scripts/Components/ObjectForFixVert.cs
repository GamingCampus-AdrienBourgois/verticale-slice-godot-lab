using Godot;
using System;

public partial class ObjectForFixVert : Node
{
	// Meme script que objectToFix donc les réunir ? 
	// Faire un seul et meme object à prendre et mettr un sprite en export et voila, et change les tag dans l'inspecteur
	private int PedestalsActives = 0;
	private int PedestalsToActive = 0;
	public bool STATE = false;

	public override void _Ready()
	{
		foreach (Node i in GetNode<Node>("Areas").GetChildren())
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
		if(body.IsInGroup("FixVert"))
		{
			if(ObjectFixedArea.GetState() == false)
			{
				ObjectFixedArea.Call("FixedChange");
				PedestalsActives++;
			}
			// Sinon au lieu de ça, faire un foreach des enfants et check leur state
			if (PedestalsActives == PedestalsToActive){
				GD.Print("FixVert activated");
				STATE = true;
				// Changer la variable dans le global ou faire un emit au main
			}
		}
	}

	private void PedestalDesactivated(ObjectFixArea Area, Node2D body) 
	{
		if(body.IsInGroup("FixVert"))
		{
			if(Area.GetState() == true)
			{
				Area.Call("FixedChange");
				PedestalsActives--;
				STATE = false;
			}

		}
	}
}
