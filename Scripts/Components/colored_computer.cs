using Godot;
using System;

public partial class colored_computer : StaticBody2D
{
	[Export]
	public int colorStatus = 0;
	
	[Export]
	Color RedColor = Color.Color8(255,0,0);
	[Export]
	Color OrangeColor = Color.Color8(255,104,0);
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

	PointLight2D light = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		light = GetNode<PointLight2D>("PointLight2D");
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
				spriteAnim.Play("Black");
				light.Visible = false;
				break;
			case 1:
				light.Color = RedColor;
				spriteAnim.Play("Red");
				light.Visible = true;
				break;
			case 2:
				light.Color = OrangeColor;
				spriteAnim.Play("Orange");
				break;
			case 3:
				light.Color = YellowColor;
				spriteAnim.Play("Yellow");

				break;
			case 4:
				light.Color = GreenColor;
				spriteAnim.Play("Green");
				break;
			case 5:
				light.Color = BlueColor;
				spriteAnim.Play("Blue");
				break;
			case 6:
				light.Color = PurpleColor;
				spriteAnim.Play("Purple");
				break;
			default:
				spriteAnim.Play("Black");
				light.Visible = false;
				break;
		}
	}
	
}


