using Godot;
using System;
using System.Numerics;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float PUSH = 0.5f;

	private Node2D pickedUpItem;
	private Node2D item = null;
	private CollisionShape2D pickedUpItem_collision = null;

	[Export]
	public bool inputOnFocus = false; // Permet de désactiver les mouvements quand le joueur est dans une interface

	public override void _Process(double delta)
	{
		Godot.Vector2 velocity = Velocity; // En gd script y a pas ça 
		
		// Prend les input
		Godot.Vector2 input_direction = new(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));

		input_direction = input_direction.Normalized(); // Permet de pas aller plus vite en diagonale

		// Si il y a des inputs
		if (input_direction != Godot.Vector2.Zero && inputOnFocus == false)
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

		if (Input.IsActionJustPressed("interact") && inputOnFocus == false)
		{
			if(item != null)
			{
				if (item.IsInGroup("PC"))
				{
					computer temp = (computer)item;
					InteractWithComputer(temp);
				}
				else if(item.IsInGroup("ColoredPC")){
					colored_computer temp = (colored_computer)item;
					InteractWithColoredComputer(temp);
				}
			}
				
		}

		if (Input.IsActionJustPressed("ui_accept") && inputOnFocus == false){
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
		// S2
		//pickedUpItem_collision = objectToPickup.GetNode<CollisionShape2D>("CollisionShape2D");
		//GD.Print(pickedUpItem_collision.Position);
		//objectToPickup.RemoveChild(pickedUpItem_collision);

		// S1
		GetParent().RemoveChild(objectToPickup);
		GetNode<Marker2D>("Object").AddChild(objectToPickup);
		objectToPickup.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true; 
		objectToPickup.Position = Godot.Vector2.Zero;
		pickedUpItem = objectToPickup;
		item = null;
	}

	private void Throw(){


		//GetParent().GetNode(pickedUpItem.Name.ToString()).AddChild(pickedUpItem_collision);

		pickedUpItem.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false; 
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
			}
			else if (body.IsInGroup("PC") || body.IsInGroup("ColoredPC")){
				item = body;
			}
		}
		
	}
	private void _on_area_2d_body_exited(Node2D body)
	{
		if (item == body){
			if (body.IsInGroup("Pickable") || body.IsInGroup("PC") || body.IsInGroup("ColoredPC")){
				item = null;
			}
		}
	}
	
	private void InteractWithComputer(computer interactingObject)
	{
		if(interactingObject.Opened == true)
		{
			interactingObject.ComputerOpenedChange();
		}
	}

	private void InteractWithColoredComputer(colored_computer interactingObject)
	{
		interactingObject.ComputerColorChange();
	}

}

