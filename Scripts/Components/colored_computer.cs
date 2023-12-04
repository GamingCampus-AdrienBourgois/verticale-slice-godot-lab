using Godot;
using System;

public partial class colored_computer : StaticBody2D
{
	[Export]
	public int colorStatus = 0;

	[Export]
	Color RedColor = new Color(255,0,0);
	[Export]
	Color OrangeColor = new Color(255,104,0,255);
	[Export]
	Color YellowColor = new Color(255,255,0);
	[Export]
	Color GreenColor = new Color(0,255,0);
	[Export]
	Color BlueColor = new Color(0,0,255);
	[Export]
	Color PurpleColor = new Color(122,0,255);
	[Export]
	Color NoColor = new Color(0,0,0);

	// Get la node collision de Computer
	CollisionShape2D collision = null;
	Sprite2D sprite = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		collision = GetNode<CollisionShape2D>("CollisionShape2D");
		collision.Disabled = false;
		changeScreenColor();
	}

	public void ComputerColorChange(){
		if (colorStatus == 6){
			colorStatus = 0;
		}
		else {
			colorStatus++;
		}
		changeScreenColor();
	}

	private void changeScreenColor(){
		switch (colorStatus)
		{
			case 0:
				sprite.Modulate = NoColor;
				break;
			case 1:
				sprite.Modulate = RedColor;
				break;
			case 2:
				sprite.Modulate = OrangeColor;
				break;
			case 3:
				sprite.Modulate = YellowColor;
				break;
			case 4:
				sprite.Modulate = GreenColor;
				break;
			case 5:
				sprite.Modulate = BlueColor;
				break;
			case 6:
				sprite.Modulate = PurpleColor;
				break;
			default:
				sprite.Modulate = NoColor;
				break;
		}
	}
	
}


