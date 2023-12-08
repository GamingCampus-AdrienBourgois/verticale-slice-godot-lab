using Godot;
using System;

public partial class AnimationSceneTransition : AnimationPlayer
{
	[Signal]
	public delegate void AnimationFinishEventHandler();
	private void _on_animation_finished(StringName anim_name)
	{
		EmitSignal("AnimationFinish");
	}

}



