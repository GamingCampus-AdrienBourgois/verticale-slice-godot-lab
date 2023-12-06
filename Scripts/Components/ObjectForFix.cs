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
			i.Connect("ObjectFixed",new Callable(this,"ObjectFixed"));
			ObjectToFix++;
		}
	}

	public void ObjectFixed(ObjectFixArea ObjectFixedArea, Node2D body){
		if (body.IsInGroup("ObjectFix")){
			//ObjectFixedArea.FixedChange();
			ObjectFixedArea.Call("FixedChange");
			GD.Print(ObjectFixedArea.Fixed);
			
			// int ObjectFixed = 0;
			// GD.PushError("jaioejaizoe" );
			// foreach (Node i in GetChildren())
			// {
			// 	ObjectFixArea temp = (ObjectFixArea)i;
			// 	if (temp.Fixed == true){
			// 		ObjectFixed++;
			// 	};
			// }

			ObjectAlreadyFixed++;
			if (ObjectAlreadyFixed >= ObjectToFix){
				GD.Print("GOOOD");
				// Changer la variable dans le global ou faire un emit au main
			}
		}
	}


}
