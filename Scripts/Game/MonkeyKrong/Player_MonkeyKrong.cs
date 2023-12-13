using Godot;
using System;

public partial class Player_MonkeyKrong : CharacterBody2D
{
	[Export]
	public float Speed = 100.0f;
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
		Vector2 direction = new Vector2(0,0);
		if (IsOnFloor())
		{
			direction = Input.GetVector("Left", "Right", "Up", "Down");
		}
		if (Input.IsActionJustPressed("Down")){
			var newPosition = Position;
			newPosition.Y ++;
			Position = newPosition;
		}

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else if(IsOnFloor())
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed/3);
		}

		Velocity = velocity;

		MoveAndSlide();
	}
}
