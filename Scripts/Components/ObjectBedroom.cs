using Godot;
using System;

public partial class ObjectBedroom : CharacterBody2D
{

	[Export]
	private string GroupToAdd;


	[Export]
	string anim = null;

	AnimatedSprite2D sprite = null;

	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		AddToGroup(GroupToAdd);
		sprite.Play(anim);
	}
}
