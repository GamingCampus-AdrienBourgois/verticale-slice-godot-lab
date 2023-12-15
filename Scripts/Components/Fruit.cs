using Godot;
using System;

public partial class Fruit : CharacterBody2D
{

	[Export]
	int anim;

	Sprite2D sprite = null;

	// Kiwi bon fruit
	// Mettre groupe True et Kiwi pour le bon fruit
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D2");
		sprite.Frame = anim;
	}

}
