using Godot;
using System;

public partial class Fruit : CharacterBody2D
{

	[Export]
	Texture2D Sprite;

	// Kiwi bon fruit
	// Mettre groupe True et Kiwi pour le bon fruit
	public override void _Ready()
	{
		// Mettre si y a un sprite 
		if(Sprite.GetImage() != null) 
		{
			GetNode<Sprite2D>("Sprite2D").Texture = Sprite;
			GetNode<Polygon2D>("Polygon2D").QueueFree();
		}
	}

}
