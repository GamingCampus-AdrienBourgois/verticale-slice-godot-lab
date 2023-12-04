using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Arrow_fix_wire_hud : Sprite2D
{

	private Sprite2D sprite;
	private int y = 200;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Arrow");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 input_direction = new(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));
		
		input_direction = input_direction.Normalized();

		if (input_direction != Vector2.Zero && sprite.Position.Y == 400)
		{
			y += 100;
			sprite.Position = new Vector2(200,y);
		} else if (sprite.Position.Y >= 400)
		{
			sprite.Position = new Vector2(200, 200);

		}
	}
}
