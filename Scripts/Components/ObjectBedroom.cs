using Godot;
using System;

public partial class ObjectBedroom : CharacterBody2D
{

	[Export]
	private string GroupToAdd;
	[Export]
	private Texture2D sprite;
	
	public override void _Ready()
	{
		AddToGroup(GroupToAdd);
		//GetNode<Sprite2D>("Sprite2D").Texture = sprite;
	}
}
