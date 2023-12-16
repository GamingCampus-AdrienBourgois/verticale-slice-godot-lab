using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

public partial class DialogueHUD : CanvasLayer
{

    [Export]
    public bool Show;

    private Label txt_label;
    private AnimationPlayer animation;
    private AudioStreamPlayer audio;

    private int DialogID = 0;
    private bool isShow = true;
    private bool hasReset = false;
    private bool hasAnimationstarted = false;
    private bool DialogReadyToNext = false;

    private string save_file_path = "user://Data/SavedData.dat";
    private string dialogue_file_path = "res://Data/Dialogue.txt";
    private List<string> dialogueLines;

    public override void _Ready()
    {
        txt_label = GetNode<Label>("HBoxContainer/ColorRect2/Sprite2D/Label");
        animation = GetNode<AnimationPlayer>("AnimationPlayer");
        audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        if (Show)
        {
            Visible = isShow;
        }
        else
        {
            isShow = false;
            Visible = isShow;
        }
        DialogID = LoadDialogID();

        dialogueLines = ReadDialogLine(dialogue_file_path);
        
        if (!hasReset)
        {
            ResetDialogID();
            hasReset = true;
        }
    }

    public override void _Process(double delta)
    {
        Visible = isShow;
        if (isShow && Show)
        {
            TextUpdate();
            if (Input.IsActionJustPressed("ui_accept") && !DialogReadyToNext)
            {
                DialogReadyToNext = true;
                animation.Play("Skip");

            }
            else if (Input.IsActionJustPressed("ui_accept") && DialogReadyToNext)
            {
                DialogID++;
                SaveDialogID();
                hasAnimationstarted = false;
                DialogReadyToNext = false;
            }

            if (Input.IsActionJustPressed("ui_cancel")) // A retirer une fois les dialog fini
            {
                ResetDialogID();
                DialogID = LoadDialogID();
                hasAnimationstarted = false;
                DialogReadyToNext = false;
            }
        }
    }

    public void OnAnimationPlayerFinished()
    {
        if (!DialogReadyToNext)
        {
            DialogReadyToNext = true;
        }
    }

    private void TextUpdate()
    {
        txt_label.Text = dialogueLines[DialogID];
        if (animation.IsPlaying())
        {
            hasAnimationstarted = true;
        }
        if (!hasAnimationstarted)
        {
            animation.Play("Dialogue");
        }
        
    }

    private List<string> ReadDialogLine(string path)
    {
        List<string> lines = new List<string>();

        var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);

        while (!file.EofReached())
        {
            string line = file.GetLine();
            lines.Add(line);
        }

        file.Close();

        return lines;
    }

    public void ShowDialogHud()
    {
        isShow = true;
    }

    public void HideDialogHud()
    {
        isShow = false;
    }

    public void SaveDialogID()
    {
        using var file = FileAccess.Open(save_file_path, FileAccess.ModeFlags.Write);
        file.StoreString(DialogID.ToString());
        
    }

    public int LoadDialogID()
    {
        using var file = FileAccess.Open(save_file_path, FileAccess.ModeFlags.Read);
        string content = file.GetLine();

        if (content != null)
        {
            return content.ToInt();
        }
        else 
        { 
            return 0; 
        }
    }

    public void ResetDialogID()
    {
        using var file = FileAccess.Open(save_file_path, FileAccess.ModeFlags.Write);
        file.StoreString("0");
    }
}
