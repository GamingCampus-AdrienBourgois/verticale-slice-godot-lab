using Godot;
using System;

public partial class CameraUI : Camera2D
{
	public override async void _Ready()
	{
		Node Container = GetNode("CanvasLayer/PanelContainer/HBoxContainer");
		Global global = GetTree().Root.GetNode<Global>("Global");
		if(GetTree().Root.GetNode<Global>("Global").Message != "")
		{
			Container.GetNode<Label>("Portal").Text = "Portal :" + global.Message;
		}
		else	
		{
			Container.GetNode<Label>("Portal").Text = "Portal : No current message";
		}
		await ToSignal(GetTree().CreateTimer(0.1),"timeout");
		Container.GetNode<Label>("Complete").Text = "   Fixed :"+global.LevelFinished + "/" + global.List_Level_Bools.Count;
	}
}
