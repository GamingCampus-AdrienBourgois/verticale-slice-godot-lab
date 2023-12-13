using Godot;
using System;
using System.Diagnostics.Tracing;
using System.Linq;

public partial class The_end : Node2D
{
	Control textrpg = null;
	Label speaktext = null;
	AnimationPlayer animation_text = null;
	[Export]
	private string[] AllText;
	int TextNumber = 0;
	bool animation_finished = true;

	public override void _Ready()
	{

		//AllText.Append("Hmmmmm");

		// Si j'ai envie, faire un .txt et rajouter chaque ligne à la liste



		textrpg = GetNode<Control>("TextRpg");	
		speaktext = textrpg.GetNode("PanelContainer").GetNode<Label>("SpeakText");
		animation_text = speaktext.GetNode<AnimationPlayer>("AnimationPlayer");
	}

	private async void _on_area_2d_body_entered(Node2D body)
	{
		textrpg.Visible = true;
		PlayAnimation();
		await ToSignal(animation_text, AnimationPlayer.SignalName.AnimationFinished);
		//await ToSignal(animation_text, AnimationPlayer.SignalName.AnimationFinished);
	}

	public async override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Accept") && textrpg.Visible == true && animation_text.CurrentAnimation == "")
		{
			//var appear = new Animation();
			// var track_index = appear.AddTrack(Animation.TrackType.Animation);
			// appear.TrackSetPath(track_index,"speaktext.VisibleRatio");
			// appear.TrackInsertKey(track_index,0.0,0);
			// appear.TrackInsertKey(track_index,10f/speaktext.Text.Length,1);

			//GD.Print("Animation :",animation_text.GetAnimation("appear"));
			//var track_animation_index = animation.TrackGetPath(0);
			//GD.Print("Track :",animation.FindTrack("../SpeakText:visible_ratio",Animation.TrackType.Value));
			//animation.TrackGetPath(0);
			//GD.Print(animation.GetTrackCount());


			// En gros, ça met les keys, mais ça met instant de 0 à 1
			var animation = animation_text.GetAnimation("appear");
			//animation.TrackRemoveKey(0 , 0);
			//animation.TrackInsertKey(0 , 0 , 0 , 1);
			//animation.TrackRemoveKey(0 , 1);
			//animation.TrackInsertKey(0 , 5f/speaktext.Text.Length , 1 , 1);

			animation.TrackSetKeyTime(0 , 1 , 2);

			
			//GD.Print("Trackname method :",animation.MethodTrackGetName(0,0));

			//GD.Print(animation.TrackInsertKey(0,10f/speaktext.Text.Length,1));

			// animation.TrackRemoveKey(track_animation_index,1);
			// animation.TrackInsertKey(track_animation_index,1f/speaktext.Text.Length,1);


			//animation_text.SpeedScale = CalculSpeed(10);
			animation_text.SpeedScale = 1;
			animation_text.PlayBackwards("appear");
			await ToSignal(animation_text, AnimationPlayer.SignalName.AnimationFinished);
			TextNumber++;
			speaktext.Text = "";
			PlayAnimation();
		}
		else if (@event.IsActionPressed("Accept") && textrpg.Visible == true && animation_text.CurrentAnimation != "")
		{
			animation_text.SpeedScale = 3;
		}
	}


	private float CalculSpeed(int TextLength)
	{
		float speed = 1;
		//float substract = 10f/Text.Length;
		//GD.Print("sub :",substract);
		//speed -= substract;
		GD.Print("speed :",speed);
		speed = speed/(10f/TextLength);
		return speed;
	}

	private void PlayAnimation()
	{
		if(AllText.Length > TextNumber)
		{
			// Met bien la key mais pas de ease entre les 2, ça va de 0 à 1 direct
			speaktext.Text = AllText[TextNumber];
			var animation = animation_text.GetAnimation("appear");
			// animation.TrackRemoveKey(0,1);
			// animation.TrackInsertKey(0,1,1,1);
			// GD.Print("KeyCount :",animation.TrackGetKeyCount(0));
			// GD.Print("Key Value 0 :",animation.TrackGetKeyValue(0,0));
			// animation.TrackSetKeyValue(0,1,1);
			// GD.Print("Value Key 1:",animation.TrackGetKeyValue(0,1));
			// GD.Print("Time Key 1:",animation.TrackGetKeyTime(0,1));
			// GD.Print("KeyCount :",animation.TrackGetKeyCount(0));
			//GD.Print(0.5f+((float)speaktext.Text.Length/20));
			// Vu que l'animation ne va pas au dessus de 1 bah ça peut pas
			float time = 0.5f+((float)speaktext.Text.Length/20);
			animation.Length = time;
			animation.TrackSetKeyTime(0 , 1 , time);
			animation_text.SpeedScale = 1;
			animation_text.Play("appear");


			// Vu que l'anim dépassait pas 1 sec pour ça que ça marchait pas bien avant



		}
	}

}
