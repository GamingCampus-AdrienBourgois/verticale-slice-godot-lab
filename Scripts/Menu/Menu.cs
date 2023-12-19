using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

public partial class Menu : Control
{

	private int SelectedIndex = 0;
	private List<NodePath> List_labels;
	private Scene_transition SceneTransition = null;
	private AudioStreamPlayer audio = null;
	private Node buttons = null;
	private int MaxIndex = 0;

	public override void _Ready()
	{
		//ChangeColor();
		MaxIndex = GetNode<Node2D>("Buttons").GetChildren().Count() - 1;
		ChangeAnim();	
		audio = GetNode<AudioStreamPlayer>("Audio");
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		buttons = GetNode<Node2D>("Buttons");


		// Veut pas prendre ça dans une liste

		// foreach (Node i in GetNode<Node>("Container").GetChildren())
		// {
		// 	GD.Print(i.Name);
		// 	GD.Print(i.GetPath());
		// 	List_labels.Add(null); // S'arrete à la vu qu'il bug
		// 	// Object reference not set to an instance of an object

		// }
	}

	public override void _Input(InputEvent @event)
	{
	   if(@event.IsActionPressed("Right"))
	   {
			ChangeSelecInd(+1);
	   }
	   if(@event.IsActionPressed("Left"))
	   {
			ChangeSelecInd(-1);
	   }
	   if(@event.IsActionPressed("Up"))
	   {
			ChangeSelecInd(-1);
	   }
	   if(@event.IsActionPressed("Down"))
	   {
			ChangeSelecInd(+1);
	   }
	   if (@event.IsActionPressed("ui_accept"))
	   {
			GD.Print(SelectedIndex);
			//Mettre un switch mais jsp la syntaxe
			if (SelectedIndex == 0)
			{
				SceneTransition.Call("changeScene","Scenes/Components/DialogueHUD.tscn",false);
			}
			else if(SelectedIndex == 1)
			{
				SceneTransition.Call("changeScene","Scenes/Menu/Input_settings.tscn",false);
			}
			else if(SelectedIndex == 2)
			{
				GetTree().Quit();
			}
			else if(SelectedIndex == 3)
			{
				SceneTransition.Call("changeScene","Scenes/Game/Tutorial.tscn",false);
			}
	   }
	   GD.Print(SelectedIndex);
		ChangeAnim();
		//ChangeColor();

	   //GetNode<Godot.Label>(List_labels[SelectedIndex]).SelfModulate = new Color(0.545098f, 0, 0, 1);
	}

	private void ChangeSelecInd(int i)
	{
		SelectedIndex += i; 
		if(SelectedIndex < 0)
		{
			SelectedIndex = 2;
		}
		else if (SelectedIndex > MaxIndex)
		{
			SelectedIndex = 0;
			
		}
		audio.Play();
	}

	private void ChangeAnim()
	{
		int y = 0;
		// Ne veut pas faire le buttons.GetChildren() Pourquoi ? Bonne question
		// Le for i ne marche pas ? Je sais pas
		// Les enfants étaient inversés pour start et quit -> balle dans la tete

		//Possible de faire des sprite2D et de changer la frame, mais si on a des animations ce sera plus opti
		foreach (AnimatedSprite2D x in GetNode<Node2D>("Buttons").GetChildren())
		{
			if (y == SelectedIndex)
			{
				x.Play("ON");
			}
			else {
				x.Play("OFF");
			}
			y++;
		}
	}


	//private void ChangeColor(){
	//	for (int x = 0; x < GetNode<Node>("Container").GetChildren().Count; x++)
	//	{
	//		if (x == SelectedIndex)
	//		{
	//			GetNode<Node>("Container").GetChild<Godot.Label>(x).SelfModulate = new Color(255, 0, 0);
	//		}
	//		else {
	//			GetNode<Node>("Container").GetChild<Godot.Label>(x).SelfModulate = new Color(255, 255, 255);
	//		}
	//	}
	//}



}
