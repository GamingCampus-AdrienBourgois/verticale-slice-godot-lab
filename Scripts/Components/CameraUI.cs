using Godot;
using System;

public partial class CameraUI : Camera2D
{
	public override void _Ready()
	{
		if(GetTree().Root.GetNode<Global>("Global").Message != "")
		{
			GetNode<Label>("PanelContainer/Label").Text = "Portal :" + GetTree().Root.GetNode<Global>("Global").Message;
		}
		else	
		{
			GetNode<Label>("PanelContainer/Label").Text = "Portal : No current message";
		}
	}
}
