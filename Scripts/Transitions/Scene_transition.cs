using Godot;
using System;

public partial class Scene_transition : CanvasLayer
{
	//AnimationPlayer Animations = null;

	private String Target = null;


	public void changeScene(String target)
	{
		// foreach (Node i in GetChildren())
		// {
		// 	GD.Print(i.Name);
		// }
		//Animations = GetNode<AnimationPlayer>("Animation");

		Target = target;
		GD.Print(GetType());
		GD.Print(GetChildCount());
		if (GetNode("Animation") != null)
		{
			GetNode<AnimationPlayer>("Animation").Play("dissolve");
			GetNode<AnimationPlayer>("Animation").Connect("AnimationFinish", new Callable(this, "_OnAnimationFinished"));
		}
		else
		{
			GD.PrintErr("Animations is null. Ensure AnimationPlayer node is present in the scene and named correctly.");
		}
		

	}
	public void _OnAnimationFinished()
	{
		GetTree().ChangeSceneToFile(Target);
		GetNode<AnimationPlayer>("Animation").PlayBackwards("dissolve");
		GetNode<AnimationPlayer>("Animation").Disconnect("AnimationFinish", new Callable(this, "_OnAnimationFinished"));
	}

}
