using Godot;
using System;

public partial class computer : StaticBody2D
{
	[Export]
	public bool Opened = true;

	[Export]
	Color OpenedColor = new Color(255,0,255);
	[Export]
	Color NotOpenedColor = new Color(0,0,0);

	// Get la node collision de Computer
	CollisionShape2D collision = null;
	Sprite2D sprite = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		collision.Disabled = false;
		if (Opened == false){
			sprite.Modulate = NotOpenedColor;
		}
		else {
			sprite.Modulate = OpenedColor;
		}
	}

	public void ComputerOpenedChange(){
		if (Opened == true){
			Opened = false;
			this.Modulate = NotOpenedColor;
		}
	}
}


