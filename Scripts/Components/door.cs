using Godot;
using System;

public partial class door : StaticBody2D
{

	[Export]
	bool Opened = false;
	
	[Export]
	bool DoorWithLight = false;

	[Export]
	Color OpenedColor = new Color(141,94,0);
	[Export]
	Color NotOpenedColor = new Color(100,50,0);
	
	[Export]
	string animOpen = null;
	[Export]
	string animClosed = null;

	AnimatedSprite2D sprite = null;

	[Signal]
	public delegate void DoorChangeEventHandler();


	// Get la node collision de door
	CollisionShape2D collision = null;

	[Export]
	int anim_to;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//sprite = GetNode<Sprite2D>("sprite");
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		collision.Disabled = Opened;
		if (collision.Disabled == true){
			//sprite.Modulate = NotOpenedColor;
			GetNode<Node2D>("Top").Visible = false;
			GetNode<Node2D>("Right").Visible = false;
		}
		else {
			if(anim_to == 0)
			{
				GetNode<Node2D>("Top").Visible = true;
				GetNode<Node2D>("Right").Visible = false;
			}
			else
			{
				GetNode<Node2D>("Right").Visible = true;
				GetNode<Node2D>("Top").Visible = true;
			}
			//sprite.Modulate = OpenedColor;
		}
		if(DoorWithLight){
			sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			UpdateAnimLight();
		}
	}

	public void DoorOpenedChange(){
		if (collision.Disabled == true){
			
			if(anim_to == 0)
			{
				GetNode<Node2D>("Top").Visible = true;
			}
			else
			{
				GetNode<Node2D>("Right").Visible = true;
			}
			collision.Disabled = false;
			//sprite.Modulate = NotOpenedColor;
			//sprite.Modulate = OpenedColor;
		}
		else {
			sprite.Play(animClosed);
			if(anim_to == 0)
			{
				GetNode<Node2D>("Top").Visible = false;
			}
			else
			{
				GetNode<Node2D>("Right").Visible = false;
			}
			collision.Disabled = true;
			//sprite.Modulate = OpenedColor;
			//sprite.Modulate = NotOpenedColor;
		}
		if(DoorWithLight){
			UpdateAnimLight();
		}
	}

	public void UpdateAnimLight(){
		if (collision.Disabled == true){
			sprite.Play(animOpen);
		}
		else
		{
			sprite.Play(animClosed);
		}
	}
}
