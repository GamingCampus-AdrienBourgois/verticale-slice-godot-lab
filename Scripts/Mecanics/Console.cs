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
	private List<string> List_levels_texts = new List<string>();
	private string TpTo = null;

	private bool GameFinished = false;

	// Pour l'anim, mettre des nombres entre 0 et 1 aléatoire et ça les rajoute dans le text

	public override void _Ready()
	{
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		global = GetParent().GetNode<Global>("Global");
		Vbox = GetNode("Console_UI").GetNode<Node>("VBoxContainer");
		label = Vbox.GetNode<Label>("Label");

		List_levels = global.List_Level_Bools;
		List_levels_texts = global.List_Level_Texts;
		TpTo = global.TpEnd;



		// Faire une list dans le global avec des bools etc, quand passe portail
		// Passe la list des bools dans le global et ici reprends celle-ci
		// Deuxieme liste qui est passé avec des textes
		// Lit la liste et si break lit le text de l'autre list

		int count = 0;
		// Refaire ça pour que ça prenne le text lié au niveau -> Dictionnaire à faire
		foreach(bool i in List_levels){
			if (i == false)
			{
				label.Text = List_levels_texts[count];
				global.Message = label.Text;
				GD.Print("Still task");
				break;
			}
			count++;
			if(count == List_levels.Count)
			{
				GD.Print("Finished");
				label.Text = "BIP BIP BIP BIIIIIIIIIIP PORTAL ACTIVATED ";
				GameFinished = true;
				break;	
			}
		}
		int countBool = 0;
		foreach(bool i in List_levels){
			if (i == true)
			{
				countBool++;
			}
		}
		global.LevelFinished = countBool;

	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_accept"))
		{
			if(GameFinished)
			{
				SceneTransition.Call("changeScene",TpTo,false);
			}
			else
			{
				SceneTransition.Call("changeScene",global.TpOut,false);
			}
		}
	}

}
