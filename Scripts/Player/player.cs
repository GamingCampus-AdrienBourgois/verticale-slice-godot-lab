using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 300.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity; // En gd script y a pas ça 
		
		// Prend les input
		Vector2 input_direction = new(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));

		input_direction = input_direction.Normalized(); // Permet de pas aller plus vite en diagonale

		// Si il y a des inputs
		if (input_direction != Vector2.Zero)
		{
			velocity= input_direction * Speed; 
		}

		// Si il y en a pas
		else
		{
			// Flemme de réunir en une ligne
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		// Aussi pas en gd script vu que ça a été simplifié
		Velocity = velocity;
		MoveAndSlide();
	}
}
