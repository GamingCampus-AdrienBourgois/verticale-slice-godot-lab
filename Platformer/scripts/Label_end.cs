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
			GetParent().GetParent().GetParent().GetNode("End").QueueFree();
			animations.Play("Show_text");
			await ToSignal(animations,AnimationPlayer.SignalName.AnimationFinished);
			animations.PlayBackwards("Show_text");
			await ToSignal(animations,AnimationPlayer.SignalName.AnimationFinished);
			QueueFree();
		}
	}

}

