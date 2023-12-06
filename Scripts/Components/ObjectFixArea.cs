using Godot;
using System;

public partial class ObjectFixArea : Area2D
{
	[Signal]
	public delegate void ObjectFixedEventHandler(ObjectFixArea This,Node2D body);
	public bool Fixed = false;

	public void FixedChange(){
		if(Fixed){
			Fixed = false;
		}
		else if (!Fixed){
			Fixed = true;
		}
	}

	private void _on_body_entered(Node2D body)
	{
		GD.Print("Type node : ",GetType());
		EmitSignal("ObjectFixed",this,body);
	}


}


