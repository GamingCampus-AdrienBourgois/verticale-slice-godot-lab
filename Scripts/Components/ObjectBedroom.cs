using Godot;
using System;

public partial class ObjectBedroom : CharacterBody2D
{

	[Export]
	private string GroupToAdd;

	public override void _Ready()
	{
		AddToGroup(GroupToAdd);
	}
}
