using Godot;
using System;

public partial class NotTp_TP : Area2D
{
	public override void _Ready()
	{
		Connect(Area2D.SignalName.BodyEntered,new Callable(this,"OnBody"));
	}
	private void OnBody(Node2D body)
	{
		if(body.IsInGroup("Player"))
		{
			if(Name == "In")
			{
				body.GlobalPosition = GetParent().GetNode("Out").GetNode<Node2D>("Marker2D").GlobalPosition;
			}
			if(Name == "Out")
			{
				body.GlobalPosition = GetParent().GetNode("In").GetNode<Node2D>("Marker2D").GlobalPosition;
			}
		}

	}
}
