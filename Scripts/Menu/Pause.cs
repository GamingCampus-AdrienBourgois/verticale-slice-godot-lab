using Godot;
using System;


public partial class Pause : Control
{
	private int SelectedIndex = 0;
	private Scene_transition SceneTransition = null;
	private bool paused = false;

	public override void _Ready()
	{
		paused = GetParent().GetParent<Main>().paused;
	}

	public override void _Input(InputEvent @event)
	{
		if(paused)
		{
			if(Input.IsActionPressed("ui_right"))
			{
				ChangeSelecInd(+1);
			}
			if(Input.IsActionPressed("ui_left"))
			{
				ChangeSelecInd(-1);
			}
			if(Input.IsActionPressed("ui_up"))
			{
				ChangeSelecInd(-1);
			}
			if(Input.IsActionPressed("ui_down"))
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
					GetTree().Quit();
				}
			}

			ChangeColor();
		}
	}

	private void ChangeSelecInd(int i){
		SelectedIndex += i; 
		if(SelectedIndex < 0)
		{
			SelectedIndex = 1;
		}
		else if (SelectedIndex > 1)
		{
			SelectedIndex = 0;
			
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

	public void UpdatePause()
	{
		paused = GetParent().GetParent<Main>().paused;
	}
}
