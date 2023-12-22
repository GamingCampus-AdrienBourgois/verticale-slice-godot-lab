using Godot;
using System;
using System.Collections.Generic;

public partial class computer : StaticBody2D
{

	[Signal]
	public delegate void ComputerChangedEventHandler();

	[Export]
	public bool Opened = true;

	[Export]
	Color OpenedColor = Color.Color8(255,255,255);
	[Export]
	Color NotOpenedColor = new Color(0,0,0);
	[Export]
	public bool IsLocked = false;

	// Get la node collision de Computer
	CollisionShape2D collision = null;
	Sprite2D sprite = null;

	AnimatedSprite2D animatedsprite = null;
	[Export]
	string anim = "Top";
	List<string> colors = new List<string>();
	AudioStreamPlayer audio = null;

	PointLight2D light = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		colors.Add("Blue");
		colors.Add("Orange");
		colors.Add("Yellow");
		colors.Add("Purple");
		colors.Add("Green");
		colors.Add("Red");

		animatedsprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		light = GetNode<PointLight2D>("PointLight2D");

		audio = GetNode<AudioStreamPlayer>("Audio");
		sprite = GetNode<Sprite2D>("Sprite2D");
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		collision.Disabled = false;
		if (Opened == false){
			animatedsprite.Play(anim+"_OFF");
			light.Visible = false;
		}
		else {
			light.Visible = true;
			light.Color = OpenedColor;
			if(colors.Contains(anim))
			{
				animatedsprite.Play(anim);
			}
			else
			{	
				animatedsprite.Play(anim+"_ON");
			}
		}
	}

	public void ComputerOpenedChange(){
		if (Opened == true){
			Opened = false;
			light.Visible = false;
			if(colors.Contains(anim))
			{
				animatedsprite.Play("Top_OFF");
			}
			else
			{
				animatedsprite.Play(anim+"_OFF");
			}
			//this.Modulate = NotOpenedColor;
			audio.Play();
			EmitSignal("ComputerChanged");
		}
	}

	public void ShowMorseCodeHud()
	{
		GetParent().GetParent().GetNode<MorseCodeHud>("MorseCodeHud").ShowHud();
	}
}


