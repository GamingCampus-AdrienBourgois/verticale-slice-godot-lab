using Godot;
using System;

public partial class Light : Node2D
{
	PointLight2D Light1 = null;
	PointLight2D Light2 = null;

	[Export]
	private float scale = 0.5f;

	public override void _Ready()
	{
		Light1 = GetNode<PointLight2D>("Light2D");
		Light2 = GetNode<PointLight2D>("Light2D2");

		Light1.TextureScale = scale;
		Light2.TextureScale = scale;
	}
}
