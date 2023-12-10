using Godot;
using System;

public partial class ObjectFixArea : Area2D
{
	[Signal]
	public delegate void ONEventHandler(ObjectFixArea This,Node2D body);

	[Signal]
	public delegate void OFFEventHandler(ObjectFixArea This,Node2D body);
	private bool State = false;

	[Export]
	private Color ColorThis = new Color(100,100,0);

	public bool GetState(){ return State;}

	public override void _Ready()
	{
		Modulate = ColorThis;
	}

	public void FixedChange(){
		if(State){
			State = false;
		}
		else if (!State){
			State = true;
		}
	}

	private void _on_body_entered(Node2D body)
	{
		EmitSignal("ON",this,body);
	}

	private void _on_body_exited(Node2D body)
	{
		EmitSignal("OFF",this,body);
	}


}