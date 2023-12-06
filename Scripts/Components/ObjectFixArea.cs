using Godot;
using System;

public partial class ObjectFixArea : Area2D
{
	[Signal]
	public delegate void ObjectFixedEventHandler(ObjectFixArea This,Node2D body);
	public bool Fixed = false;

	[Export]
	private Color ColorThis = new Color(100,100,0);

	public override void _Ready()
	{
		Modulate = ColorThis;
	}

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
		EmitSignal("ObjectFixed",this,body);
	}


}


