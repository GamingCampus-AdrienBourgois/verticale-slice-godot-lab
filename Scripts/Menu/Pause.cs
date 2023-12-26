using Godot;
using System;


public partial class Pause : Control
{
	[Signal]
	public delegate void PauseMenuEventHandler();
	private int SelectedIndex = 0;
	private Scene_transition SceneTransition = null;
	private bool paused = false;
	public override void _Ready()
	{
		paused = GetParent().GetParent<Main>().paused;
		SceneTransition = GetTree().Root.GetNode<Scene_transition>("SceneTransition");
	}

	public override void _Input(InputEvent @event)
	{
		if(paused)
		{
			if(Input.IsActionPressed("Right"))
			{
				ChangeSelecInd(+1);
			}
			if(Input.IsActionPressed("Left"))
			{
				ChangeSelecInd(-1);
			}
			if(Input.IsActionPressed("Up"))
			{
				ChangeSelecInd(-1);
			}
			if(Input.IsActionPressed("Down"))
			{
				ChangeSelecInd(+1);
			}
			if (Input.IsActionPressed("ui_accept"))
			{
				//Mettre un switch mais jsp la syntaxe
				if (SelectedIndex == 0)
				{
					GetParent().GetParent<Main>().PauseMenu();
				}
				else if(SelectedIndex == 1)
				{
					GetParent().GetParent<Main>().PauseMenu();
					SceneTransition.Call("changeScene","Scenes/Menu/Menu.tscn",false);
				}
				else if(SelectedIndex == 2)
				{
					GetTree().Quit();
				}
			}

			ChangeAnim();
		}
	}

	private void ChangeSelecInd(int i){
		SelectedIndex += i; 
		if(SelectedIndex < 0)
		{
			SelectedIndex = 1;
		}
		else if (SelectedIndex > 2)
		{
			SelectedIndex = 0;
			
		}
	}

	private void ChangeAnim()
	{
		int y = 0;
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

	public void UpdatePause()
	{
		paused = GetParent().GetParent<Main>().paused;
	}
}
