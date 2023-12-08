using Godot;
using System;

public partial class AllPc : Node
{

	public bool STATE = false;

	public override void _Ready()
	{
		foreach (Node i in GetChildren())
		{
			i.Connect("ComputerChanged",new Callable(this,"Changed"));
		}
	}

	private void Changed(){
		int Open = 0;
		foreach (computer i in GetChildren())
		{
			if(i.Opened == true){
				Open++;
			}
		}
		GD.Print(Open);
		if(Open == 0){
			STATE = true;
			GD.Print("Niveau 1 complete !");
		}
	}



}
