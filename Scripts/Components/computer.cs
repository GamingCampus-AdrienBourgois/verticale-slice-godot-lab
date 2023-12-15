using Godot;
using System;

public partial class computer : StaticBody2D
{

	[Signal]
	public delegate void ComputerChangedEventHandler();

	[Export]
	public bool Opened = true;

	[Export]
	Color OpenedColor = new Color(255,0,255);
	[Export]
	Color NotOpenedColor = new Color(0,0,0);

	// Get la node collision de Computer
	CollisionShape2D collision = null;
	Sprite2D sprite = null;

	AnimatedSprite2D animatedsprite = null;
	[Export]
	string anim;
	AudioStreamPlayer audio = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		animatedsprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");


		audio = GetNode<AudioStreamPlayer>("Audio");
		sprite = GetNode<Sprite2D>("Sprite2D");
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		collision.Disabled = false;
		if (Opened == false){
		 	animatedsprite.Play(anim+"_OFF");
		}
		else {
			animatedsprite.Play(anim+"_ON");
		}
	}

	public void ComputerOpenedChange(){
		if (Opened == true){
			Opened = false;
			//this.Modulate = NotOpenedColor;
			animatedsprite.Play(anim+"_OFF");
			audio.Play();
			EmitSignal("ComputerChanged");
		}
	}
}


