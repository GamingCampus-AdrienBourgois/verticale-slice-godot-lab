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
	private string Group;

	//[Export]
	//private Color ColorThis = new Color(100,100,0);
	
	[Export]
	string anim = null;

	AnimatedSprite2D sprite = null;

	public bool GetState(){ return State;}
	public string GetGroup(){ return Group;}

	public override void _Ready()
	{
		//Modulate = ColorThis;
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite.Play(anim);
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
		//GD.Print(body.Name);
		EmitSignal("ON",this,body);
	}

	private void _on_body_exited(Node2D body)
	{
		EmitSignal("OFF",this,body);
	}


}
