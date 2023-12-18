using Godot;
using System;

public partial class colored_computer : StaticBody2D
{
	[Export]
	public int colorStatus = 0;
	
	[Export]
	Color RedColor = Color.Color8(255,0,0);
	[Export]
	Color OrangeColor = Color.Color8(255,104,0,255);
	[Export]
	Color YellowColor = Color.Color8(255,255,0);
	[Export]
	Color GreenColor = Color.Color8(0,255,0);
	[Export]
	Color BlueColor = Color.Color8(0,0,255);
	[Export]
	Color PurpleColor = Color.Color8(122,0,255);
	[Export]
	Color NoColor = Color.Color8(0,0,0);

	// Get la node collision de Computer
	CollisionShape2D collision = null;
	Sprite2D sprite = null;
	AnimatedSprite2D spriteAnim = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		spriteAnim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
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
				spriteAnim.Play("Black");
				break;
			case 1:
				sprite.Modulate = RedColor;
				spriteAnim.Play("Red");
				break;
			case 2:
				sprite.Modulate = OrangeColor;
				spriteAnim.Play("Orange");
				break;
			case 3:
				sprite.Modulate = YellowColor;
				spriteAnim.Play("Yellow");

				break;
			case 4:
				sprite.Modulate = GreenColor;
				spriteAnim.Play("Green");
				break;
			case 5:
				sprite.Modulate = BlueColor;
				spriteAnim.Play("Blue");
				break;
			case 6:
				sprite.Modulate = PurpleColor;
				spriteAnim.Play("Purple");
				break;
			default:
				sprite.Modulate = NoColor;
				spriteAnim.Play("Black");
				break;
		}
	}
	
}


