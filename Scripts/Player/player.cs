using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float PUSH = 0.5f;

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

		for (int i = 0; i > GetSlideCollisionCount(); i++){
			GD.Print("GOOOD collision");
			KinematicCollision2D collision = GetSlideCollision(i);
			GodotObject node_collision = collision.GetCollider();
			GD.Print(node_collision.ToString());


			//if (collision.IsInGroup("jpgojezopf")){}

			//collision.GetCollider().ApplyCentralImpulse(-collision.GetNormal() * PUSH);
			

		}

		// GDscript code :

		// for i in get_slide_count():
		//		var collision = get_slide_collision()
		//		if collision.collider.is_in_group("object"):
		//			collision.collider.apply_central_impule(-collision.normal * inertia)

	}
}
