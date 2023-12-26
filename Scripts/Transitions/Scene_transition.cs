using Godot;
using System;

public partial class Scene_transition : CanvasLayer
{
	//AnimationPlayer Animations = null;

	private String Target = null;

	private bool Reload = false;


	public void changeScene(String target, bool reload = false)
	{
		// foreach (Node i in GetChildren())
		// {
		// 	GD.Print(i.Name);
		// }
		//Animations = GetNode<AnimationPlayer>("Animation");
		Reload = reload;
		Target = target;
		// GD.Print(GetType());
		// GD.Print(GetChildCount());
		if (GetNode("Animation") != null)
		{
			GetNode<AnimationPlayer>("Animation").Play("dissolve");
			//if(!GetNode<AnimationPlayer>("Animation").IsConnected("AnimationFinish",new Callable(this, "_OnAnimationFinished")))
			//{
				GetNode<AnimationPlayer>("Animation").Connect("AnimationFinish", new Callable(this, "_OnAnimationFinished"));
			//}
		}
		else
		{
			GD.PrintErr("Animations is null. Ensure AnimationPlayer node is present in the scene and named correctly.");
		}
		

	}
	public void _OnAnimationFinished()
	{
		if(!Reload)
		{
			GetTree().ChangeSceneToFile(Target);
			GetNode<AnimationPlayer>("Animation").PlayBackwards("dissolve");
			GD.Print("Disconnected");
			GetNode<AnimationPlayer>("Animation").Disconnect("AnimationFinish", new Callable(this, "_OnAnimationFinished"));
		}
		else
		{
			GetTree().ReloadCurrentScene();
			GetNode<AnimationPlayer>("Animation").PlayBackwards("dissolve");
			GetNode<AnimationPlayer>("Animation").Disconnect("AnimationFinish", new Callable(this, "_OnAnimationFinished"));
		}
	}

}
