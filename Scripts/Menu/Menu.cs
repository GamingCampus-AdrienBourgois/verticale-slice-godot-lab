using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

public partial class Menu : Control
{
	
	private int SelectedIndex = 0;

	private List<NodePath> List_labels;

	public override void _Ready()
	{
		ChangeColor();

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
	   if(@event.IsActionPressed("ui_right"))
	   {
			ChangeSelecInd(+1);
	   }
	   if(@event.IsActionPressed("ui_left"))
	   {
			ChangeSelecInd(-1);
	   }
	   if(@event.IsActionPressed("ui_up"))
	   {
			ChangeSelecInd(-1);
	   }
	   if(@event.IsActionPressed("ui_down"))
	   {
			ChangeSelecInd(+1);
	   }
	   if (@event.IsActionPressed("ui_accept"))
	   {
			//Mettre un switch mais jsp la syntaxe
			if (SelectedIndex == 0)
			{
				GetTree().ChangeSceneToFile("main.tscn");
			}
			else if(SelectedIndex == 1)
			{
				// GetTree().ChangeSceneToFile("Scenes/Menu/Options.tscn")
			}
			else if(SelectedIndex == 2)
			{
				GetTree().Quit();
			}
	   }

		ChangeColor();

	   //GetNode<Godot.Label>(List_labels[SelectedIndex]).SelfModulate = new Color(0.545098f, 0, 0, 1);
	}

	private void ChangeSelecInd(int i){
		if (SelectedIndex+i >= 0 && SelectedIndex+i <= 2)
		{
			SelectedIndex += i; 
		}
	}

	private void ChangeColor(){
		for (int x = 0; x < GetNode<Node>("Container").GetChildren().Count; x++)
		{
			if (x == SelectedIndex)
			{
				GetNode<Node>("Container").GetChild<Godot.Label>(x).SelfModulate = new Color(255, 0, 0);
			}
			else {
				GetNode<Node>("Container").GetChild<Godot.Label>(x).SelfModulate = new Color(255, 255, 255);
			}
		}
	}

}
