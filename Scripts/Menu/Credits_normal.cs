using Godot;
using System;
using System.Linq;

public partial class Credits_normal : Control
{
	AnimationPlayer animations = null;
	Label label = null;
	AudioStreamPlayer2D audio_global = null;
	[Export]
	AudioStream Music;

	[Export]
	string[] LesGens;
	public override async void _Ready()
	{
		audio_global = GetParent().GetNode<AudioStreamPlayer2D>("BgMusic");
		audio_global.Stream = Music;
		audio_global.Play();
		animations = GetNode<AnimationPlayer>("AnimationPlayer");
		label = GetNode<Label>("Names");

		LesGens.Reverse();

		for(int i = 0; i < 5; i++)
		{
			label.Text = LesGens[i];
			animations.Play("End");
			await ToSignal(animations, AnimationPlayer.SignalName.AnimationFinished);
		}
		animations.Play("thanks");
		await ToSignal(animations, AnimationPlayer.SignalName.AnimationFinished);
		GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
