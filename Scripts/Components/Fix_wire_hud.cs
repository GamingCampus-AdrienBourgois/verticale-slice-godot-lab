using Godot;
using System;

public partial class Fix_wire_hud : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public int NumberOfWires = 0;
	public override void _Ready()
	{
		foreach (Node i in GetNode<Node>("VBoxContainer").GetChildren())
		{
			NumberOfWires++;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
