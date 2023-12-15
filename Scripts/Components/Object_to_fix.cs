using Godot;
using System;

public partial class Object_to_fix : CharacterBody2D
{

	[Export]
	string anim = null;

	AnimatedSprite2D sprite = null;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		sprite.Play(anim);
	}
}
