using Godot;
using System;

public partial class Fruit : CharacterBody2D
{

	[Export]
	Polygon2D Sprite;
	// A garder comme idée
	public override void _Ready()
	{
		//GetNode<Polygon2D>("Polygon2D").Polygon = Sprite.Polygon;
	}

}
