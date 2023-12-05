using Godot;
using System;

public partial class Label_end : Label
{
	private AnimationPlayer animations = null;

	public override void _Ready()
	{
		animations = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	private async void _on_end_body_entered(Node2D body)
	{
		if (body.IsInGroup("Player")){
			animations.Play("Show_text");
			await ToSignal(animations,"AnimationFinished");
			animations.PlayBackwards("Show_text");
			await ToSignal(animations,"AnimationFinished");
			QueueFree();
		}
	}

}

