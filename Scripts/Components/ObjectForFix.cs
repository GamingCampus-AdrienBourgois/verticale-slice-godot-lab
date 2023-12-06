using Godot;
using System;

public partial class ObjectForFix : Node
{

	public int ObjectToFix = 0;
	public int ObjectAlreadyFixed = 0;

	public override void _Ready()
	{
		foreach (Node i in GetNode<Node>("Areas").GetChildren())
		{
			i.Connect("ON",new Callable(this,"ObjectFixed"));
			i.Connect("OFF",new Callable(this,"ObjectDesactivated"));
			ObjectToFix++;
		}
	}

	public void ObjectFixed(ObjectFixArea Area, Node2D body){
		if (body.IsInGroup("ObjectFix")){
			Area.Call("FixedChange");
			ObjectAlreadyFixed++;

			if (ObjectAlreadyFixed >= ObjectToFix){
				GD.Print("GOOOD");
				// Changer la variable dans le global ou faire un emit au main
			}
		}
	}

	private void ObjectDesactivated(ObjectFixArea Area, Node2D body) 
	{
		if(body.IsInGroup("ObjectFix"))
		{
			Area.Call("FixedChange");
			GD.Print(Area.Call("GetState"));
			ObjectAlreadyFixed--;
		}
	}


}
