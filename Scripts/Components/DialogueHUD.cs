using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DialogueHUD : CanvasLayer
{

	[Export]
	public new bool Show;
	[Export]
	public float scrollSpeed;

	private Label txt_label;
	private AnimationPlayer animation;
	private AnimationPlayer spaceShipeAnim;
	private AudioStreamPlayer audio;
	// private Player player;
	private AnimatedSprite2D arrow;
	private Scene_transition SceneTransition = null;
	private Camera2D camera1;
	private HBoxContainer container;

	private int DialogID = 0;
	private bool isShow = false;
	private bool hasReset = false;
	private bool hasAnimationstarted = false;
	private bool DialogReadyToNext = false;
	private bool StartDialogFinish = false;
	private bool Dialog = true;

	//private string save_file_path = "user://Data/SavedData.dat";
	//private string dialogue_file_path = "res://Data/Dialogue.txt";
	private List<string> dialogueLines = new List<string>();

	public override async void _Ready()
	{
		txt_label = GetNode<Label>("HBoxContainer/ColorRect2/Sprite2D/Label");
		animation = GetNode<AnimationPlayer>("AnimationPlayer");
		audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		// player = GetParent().GetNode<Player>("Player");
		arrow = GetNode<AnimatedSprite2D>("HBoxContainer/ColorRect2/Sprite2D/Sprite2D");
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");
		camera1 = GetNode<Camera2D>("Camera2D");
		spaceShipeAnim = GetNode<AnimationPlayer>("Sprite2D/AnimationPlayer");
		container = GetNode<HBoxContainer>("HBoxContainer");
		if (Show)
		{
			arrow.Visible = false;
			// DialogID = LoadDialogID();
			// dialogueLines = ReadDialogLine(dialogue_file_path);
			// if (dialogueLines[DialogID] == "END")
			// {
			//     CloseDialog();
			// }
			container.Visible = isShow;
			// if (!hasReset)
			// {
			//     // ResetDialogID();
			//     hasReset = true;
			//     TextUpdate();
			// }
			// GD.Print(dialogueLines[DialogID] + " at ID: " + DialogID);
			// GD.Print(hasReset);
			dialogueLines.Add("Hi, do you hear me?");
			dialogueLines.Add("Well, I just wanted to make sure you know what to do here as it's your first day here.");
			dialogueLines.Add("The portal in front of you is broken, Bob set it up wrong.");
			dialogueLines.Add("If you try to use it in its current state, you'll be dragged back here, along with everything in its proximity.");
			dialogueLines.Add("You need to find the cause of the interference, so start by looking around, and when you think all is good, go through the portal.");
			dialogueLines.Add("If you've forgotten something, the portal console will tell you.");
			dialogueLines.Add("Well, I think I've told you everything.");
			dialogueLines.Add("END");
			TextUpdate();
			spaceShipeAnim.Play("Start");
			await ToSignal(spaceShipeAnim,AnimationPlayer.SignalName.AnimationFinished);
			spaceShipeAnim.Play("Idle");
			//Dialog = true;   
			isShow = true;     
			}
		else
		{
			isShow = false;
			Visible = isShow;
		}

	}

	public override void _Process(double delta)
	{
		container.Visible = isShow;
		// if (player != null)
		// {
		//     player.inputOnFocus = isShow;
		// }


		camera1.Position += scrollSpeed * Vector2.Right;
		if (isShow && Show)
		{
			if (DialogReadyToNext)
			{
				arrow.Visible = true;
				if (Input.IsActionJustPressed("ui_accept"))
				{
					DialogID++;
					TextUpdate();
					CheckNextDialog();
					// SaveDialogID();
					hasAnimationstarted = false;
					DialogReadyToNext = false;

				}
			}
			else if (!DialogReadyToNext)
			{
				arrow.Visible = false;
				if (Input.IsActionJustPressed("ui_accept"))
				{
					DialogReadyToNext = true;
					animation.Play("Skip");
				}
			}

			if (Input.IsActionJustPressed("ui_cancel")) // A retirer une fois les dialogues finis
			{
				// ResetDialogID();
				// DialogID = LoadDialogID();
				TextUpdate();
				hasAnimationstarted = false;
				DialogReadyToNext = false;
			}
		}
	}

	private async void CheckNextDialog()
	{
		if (txt_label.Text == "END")
		{
			txt_label.Text = "";
			spaceShipeAnim.Play("End");
			await ToSignal(spaceShipeAnim,AnimationPlayer.SignalName.AnimationFinished);
			animation.Play("End");
			await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
			SceneTransition.Call("changeScene","main.tscn",false);
		}
	}

	// private void CloseDialog()
	// {
	//     isShow = false;
	//     Visible = isShow;
	// }

	public void OnAnimationPlayerFinished(string anim_name)
	{
		if (anim_name == "Dialogue")
		{
			DialogReadyToNext = true;
		}
	}

	private void TextUpdate()
	{
		txt_label.VisibleRatio = 0;
		txt_label.Text = dialogueLines[DialogID];
		if (animation.IsPlaying())
		{
			hasAnimationstarted = true;
		}
		if (!hasAnimationstarted && txt_label.Text != "END")
		{
			animation.Play("Dialogue");

		}
	}

	// private List<string> ReadDialogLine(string path)
	// {
	//     List<string> lines = new List<string>();

	//     var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);

	//     while (!file.EofReached())
	//     {
	//         string line = file.GetLine();
	//         lines.Add(line);
	//     }

	//     file.Close();

	//     return lines;
	// }

	public void ShowDialogHud()
	{
		isShow = true;
	}

	public void HideDialogHud()
	{
		isShow = false;
	}

	// public void SaveDialogID()
	// {
	//     using var file = FileAccess.Open(save_file_path, FileAccess.ModeFlags.Write);
	//     if (file != null)
	//     {
	//         file.StoreString(DialogID.ToString());
	//     }
	// }

	// public int LoadDialogID()
	// {
	//     using var file = FileAccess.Open(save_file_path, FileAccess.ModeFlags.Read);
	//     if (file == null)
	//     {
	//         return 0;
	//     }
	//     else
	//     {
	//         string content = file.GetLine();
	//         return content.ToInt();
	//     }
	// }

	// public void ResetDialogID()
	// {
	//     using var file = FileAccess.Open(save_file_path, FileAccess.ModeFlags.Write);
	//     if (file != null)
	//     {
	//        file.StoreString("0"); 
	//     }
		
	// }
}
