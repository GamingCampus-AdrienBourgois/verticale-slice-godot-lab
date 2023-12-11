using Godot;
using System;
using System.Numerics;

public partial class Player : CharacterBody2D
{
	public float Speed = 200.0f;
	private Node2D pickedUpItem;
	private NodePath pickeUpItemPath;
	private Node2D item = null;
	private CollisionShape2D pickedUpItem_collision = null;
	private AnimatedSprite2D animatedSprite = null;

	[Export]
	public bool inputOnFocus = false; // Permet de désactiver les mouvements quand le joueur est dans une interface
	private ColorCode code;

	private Marker2D MarkerObject = null;
	private Marker2D MarkerArea = null;
	private Control ui = null;
	private HBoxContainer ui_box = null;
	private Label to_label = null;
	private AnimationPlayer ui_animations = null;
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		MarkerObject = GetNode("MarkerArea").GetNode<Marker2D>("Object");
		MarkerArea = GetNode<Marker2D>("MarkerArea");
		code = GetParent().GetNode<ColorCode>("ColoredPc");

		ui = GetNode<Control>("UI");
		ui_box = ui.GetNode<HBoxContainer>("Box");
		to_label = ui_box.GetNode<Label>("To");
		ui_animations = ui.GetNode<AnimationPlayer>("Animations");
	}



	public override void _Process(double delta)
	{
		Godot.Vector2 velocity = Velocity; // En gd script y a pas ça 
		
		// Prend les input
		Godot.Vector2 input_direction = new(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up"));

		input_direction = input_direction.Normalized(); // Permet de pas aller plus vite en diagonale

		// Faire une seule fonction avec 5 params

		if(Input.IsActionPressed("ui_up")){
			MarkerArea.RotationDegrees = 270;
			MarkerObject.RotationDegrees = 90;
			if(pickedUpItem != null) 
			{
				animatedSprite.Play("Up_Arm");
			}
			else 
			{
				animatedSprite.Play("Up");
			}
		}
		if(Input.IsActionPressed("ui_down")){
			MarkerArea.RotationDegrees = 90;
			MarkerObject.RotationDegrees = 270;
			if(pickedUpItem != null) 
			{
				animatedSprite.Play("Down_Arm");
			}
			else 
			{
				animatedSprite.Play("Down");
			}
		}
		if(Input.IsActionPressed("ui_right")){
			MarkerArea.RotationDegrees = 0;
			MarkerObject.RotationDegrees = 0;
			if(pickedUpItem != null) 
			{
				animatedSprite.Play("Right_Arm");
			}
			else 
			{
				animatedSprite.Play("Right");
			}
		}
		if(Input.IsActionPressed("ui_left")){
			MarkerArea.RotationDegrees = 180;
			MarkerObject.RotationDegrees = 180;
			if(pickedUpItem != null) 
			{
				animatedSprite.Play("Left_Arm");
			}
			else 
			{
				animatedSprite.Play("Left");
			}

		}
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
					_on_area_2d_body_exited(item);
					//item.RemoveFromGroup("PC");
				}
				else if(item.IsInGroup("ColoredPC")){
					colored_computer temp = (colored_computer)item;
					InteractWithColoredComputer(temp);
				}
			}
				
		}

		if (Input.IsActionJustPressed("ui_accept") && inputOnFocus == false){
			if (item != null && pickedUpItem == null && item.IsInGroup("Pickable")){
				PickUp(item);
			}
			else if (pickedUpItem != null){
				Throw();
			}
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
	private void PickUp(Node2D objectToPickup){
		pickeUpItemPath = objectToPickup.GetParent().GetPath();
		objectToPickup.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
		
		// Tests

		//GD.Print(objectToPickup.GetPath());
		//GD.Print(GetNode(objectToPickup.GetPath()).Name);
		
		GetNode(objectToPickup.GetPath()).GetParent().RemoveChild(objectToPickup);
		MarkerObject.AddChild(objectToPickup);
		objectToPickup.Position = Godot.Vector2.Zero;
		pickedUpItem = objectToPickup;

		item = null;
	}

	private void Throw(){
		pickedUpItem.Position = MarkerObject.GlobalPosition;
		MarkerObject.RemoveChild(pickedUpItem);
		GetNode(pickeUpItemPath).AddChild(pickedUpItem);
		pickedUpItem.GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		pickedUpItem = null;
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if (item == null && pickedUpItem == null){
			if (body.IsInGroup("Pickable")){
				to_label.Text = "Space to pickup";
				ui_animations.Play("appear");
				item = body;
			}
			else if (body.IsInGroup("PC") || body.IsInGroup("ColoredPC")){
				to_label.Text = "E to interact";
				ui_animations.Play("appear");
				item = body;
			}
		}
		
	}
	private void _on_area_2d_body_exited(Node2D body)
	{
		if (item == body){
			if (body.IsInGroup("Pickable") || body.IsInGroup("PC") || body.IsInGroup("ColoredPC")){
				item = null;
				ui_animations.PlayBackwards("appear");
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
		code.CheckNewCode();
	}

}

