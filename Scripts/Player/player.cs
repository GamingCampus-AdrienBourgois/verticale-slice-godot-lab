using Godot;
using System;
using System.Numerics;

public partial class player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float PUSH = 0.5f;

	private Node2D pickedUpItem;
	private Node2D item = null;

	public override void _Process(double delta)
	{
		Godot.Vector2 velocity = Velocity; // En gd script y a pas ça 
		
		// Prend les input
		Godot.Vector2 input_direction = new(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));

		input_direction = input_direction.Normalized(); // Permet de pas aller plus vite en diagonale

		// Si il y a des inputs
		if (input_direction != Godot.Vector2.Zero)
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

		if (Input.IsActionJustPressed("ui_accept")){
			if (item != null && pickedUpItem == null){
				PickUp(item);
			}
			else if (pickedUpItem != null){
				Throw();
			}
		}

		// Aussi pas en gd script vu que ça a été simplifié
		Velocity = velocity;
		MoveAndSlide();
	}
	private void PickUp(Node2D objectToPickup){
		GetParent().RemoveChild(objectToPickup);
		GetNode<Marker2D>("Object").AddChild(objectToPickup);
		objectToPickup.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true; // Marche pas aidez moi
		objectToPickup.Position = Godot.Vector2.Zero;
		pickedUpItem = objectToPickup;
		item = null;
	}

	private void Throw(){
		pickedUpItem.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false; // Marche pas aidez moi
		pickedUpItem.Position = GetNode<Marker2D>("Object").GlobalPosition;
		GetNode<Marker2D>("Object").RemoveChild(pickedUpItem);
		GetParent().AddChild(pickedUpItem);
		pickedUpItem = null;
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if (item == null && pickedUpItem == null){
			if (body.IsInGroup("Pickable")){
				item = body;
				GD.Print("object !");
			}
		}
		
	}
	private void _on_area_2d_body_exited(Node2D body)
	{
		if (item == body){
			if (body.IsInGroup("Pickable")){
				item = null;
			}
		}
	}
	



}

