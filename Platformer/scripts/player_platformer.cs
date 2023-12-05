using Godot;
using System;

public partial class player_platformer : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	[Export]
	public  float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (Input.IsActionJustPressed("ui_down")){
			var newPosition = Position;
			newPosition.Y --;
			Position = newPosition;
		}

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed/3);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		GetTree().ChangeSceneToFile("res://Platformer/scenes/platformer.tscn");
	}

}
