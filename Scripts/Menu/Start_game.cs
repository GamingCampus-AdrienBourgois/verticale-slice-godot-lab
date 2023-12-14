using Godot;
using System;

public partial class Start_game : Control
{
	Scene_transition SceneTransition = null;
	public async override void _Ready()
	{
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		AnimationPlayer animations = GetNode<AnimationPlayer>("AnimationPlayer");
		animations.Play("start");
		await ToSignal(animations, AnimationPlayer.SignalName.AnimationFinished);
		SceneTransition.Call("changeScene","Scenes/Menu/Menu.tscn",false);
	}
	public void Start() // On peut dans l'anim de lancer cette fonction ?
	{
		GetParent().GetNode<Scene_transition>("SceneTransition").Call("changeScene","Scenes/Menu/Menu.tscn",false);
	}
}
