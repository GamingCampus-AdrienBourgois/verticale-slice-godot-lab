using Godot;
using System;

public partial class Note : CharacterBody2D
{
    [Export]
    public bool StickyNote = false;
    [Export]
    public Texture2D img;

    private CanvasLayer FolderNoteHud;
    private CanvasLayer StickyNoteHud;

    private bool isShow = false;

    public override void _Ready()
    {
        FolderNoteHud = GetNode<CanvasLayer>("FolderNote");
        StickyNoteHud = GetNode<CanvasLayer>("StickyNote");

        FolderNoteHud.Visible = isShow;
        StickyNoteHud.Visible = isShow;

        if (!StickyNote)
        {
            AddToGroup("FolderNote");

            if (img != null)
            {
                FolderNoteHud.GetNode<PanelContainer>("PanelContainer").GetNode<Sprite2D>("Sprite2D").Texture = img;
            }
        }
    }

    public override void _Process(double delta)
    {
        if (!StickyNote)
        {
            if (Input.IsActionJustPressed("ui_cancel"))
            {
                CloseFolder();
            }

        }
    }

    public void CloseFolder()
    {
        isShow = false;
        FolderNoteHud.Visible = isShow;
    }

    public void InteractFolder()
    {
        if(!isShow)
        {
            isShow = true;
            FolderNoteHud.Visible = isShow;
        }
        else
        {
            CloseFolder();
        }
    }
}
