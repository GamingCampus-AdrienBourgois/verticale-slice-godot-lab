using Godot;
using System;
using System.Collections.Generic;

public partial class Console : Node2D
{
	private Scene_transition SceneTransition = null;
	private Global global = null;
	private Node Vbox = null;
	private Label label = null;

	private List<bool> List_levels = new List<bool>();

	private bool GameFinished = false;

	// Pour l'anim, mettre des nombres entre 0 et 1 aléatoire et ça les rajoute dans le text

	public override void _Ready()
	{
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		global = GetParent().GetNode<Global>("Global");
		Vbox = GetNode("Console_UI").GetNode<Node>("VBoxContainer");
		label = Vbox.GetNode<Label>("Label");

		List_levels.Add(global.Niveau_1);
		List_levels.Add(global.Niveau_2);
		List_levels.Add(global.Niveau_3);
		List_levels.Add(global.Niveau_4);

		int count = 0;
		// Refaire ça pour que ça prenne le text lié au niveau -> Dictionnaire à faire
		foreach(bool i in List_levels){
			count++;
			if (i == false)
			{
				if (count == 1){	label.Text = "Some people don't turn off their pc...";	}
				else if (count == 2){	label.Text = "Colors means something together... Right ?";	}
				else if (count == 3){	label.Text = "Hmmm... Sometimes things aren't were they're supposed to be.";	}
				else if (count == 4){	label.Text = "Detritus on the floor... Not the best for the company.";	}
				break;
			}
			if(count == List_levels.Count)
			{
				label.Text = "BIP BIP BIP BIIIIIIIIIIP PORTAL ACTIVATED ";
				GameFinished = true;	
			}
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_accept"))
		{
			if(GameFinished)
			{
				SceneTransition.Call("changeScene","Scenes/Menu/Credits.tscn");
			}
			else
			{
				SceneTransition.Call("changeScene","main.tscn");
			}
		}
	}

}
