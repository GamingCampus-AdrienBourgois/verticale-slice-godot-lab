using Godot;
using System;
using System.Collections.Generic;

public partial class MorseCodeHud : CanvasLayer
{
    [Export]
    public string SecretCode;

    private bool isShow = false;

    private HBoxContainer hBoxContainerCenter;
    private List<Label> labelList = new ();

    private List<char> alphabet = new()
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
        'J', 'K','L', 'M', 'N', 'O', 'P', 'Q', 'R',
        'S', 'T','U', 'V', 'W', 'X', 'Y', 'Z'
    };

    private char currentLetter;
    private int currentLabelIndex = 0;

    public override void _Ready()
    {
        isShow = true;
        hBoxContainerCenter = GetNode<HBoxContainer>("Container/VBoxContainerCenter/HBoxContainer");
        //Recuperation des labels dans le HBoxContainer
        foreach (Node node in hBoxContainerCenter.GetChildren())
        {
            if (node is Label label)
            {
                labelList.Add(label);
            }
        }
        HideSprite();
        ShowSprite();


    }

    public override void _Process(double delta)
    {
        if (isShow)
        {
            //Permet au joueur de changer la lettre du label par la lettre suivante de l'alphabet en appuyant sur la fleche du haut ou du bas
            if (Input.IsActionJustPressed("ui_up"))
            {
                lettreSuivante();
            }
            else if (Input.IsActionJustPressed("ui_down"))
            {
                lettrePrecedente();
            }

            // Permet au joueur de changer de label dans la list des label en appuyant sur la fleche de droite sauf si c'est le dernier label et conserve la lettre deja presente dans le nouveau label
            if (Input.IsActionJustPressed("ui_right"))
            {
                
                labelSuivant();
            }
            //Permet au joueur de changer de label dans la list des label en appuyant sur la fleche de gauche sauf si c'est le premier label et conserve la lettre deja presente dans le nouveau label
            else if (Input.IsActionJustPressed("ui_left"))
            {
                labelPrecedent();
            }

            //Ferme le hud si le joueur appuie sur la touche "E" ou si le code est bon
            if (Input.IsActionJustPressed("Interact") || CheckCode())
            {
                isShow = false;
                Visible = isShow;
            }

        }



    }

    //Joue l'animation du sprite2D associer au label actuel
    private void PlayAnimation(string pos)
    {
        if (pos == "top")
        {
            GetNode<AnimationPlayer>("Container/VBoxContainerCenter/HBoxContainer/" + currentLabelIndex + "/AnimationPlayer").Play("pressTop");
        }
        else if (pos == "bot")
        {
            GetNode<AnimationPlayer>("Container/VBoxContainerCenter/HBoxContainer/" + currentLabelIndex + "/AnimationPlayer").Play("pressBot");
        }
        
    }

    //Affiche les sprites2D associer au label actuel
    private void ShowSprite()
    {
        HideSprite();
        GetNode<Sprite2D>("Container/VBoxContainerCenter/HBoxContainer/" + currentLabelIndex + "/Sprite2D").Visible = true;
        GetNode<Sprite2D>("Container/VBoxContainerCenter/HBoxContainer/" + currentLabelIndex + "/Sprite2D2").Visible = true;
    }

    //Cache les sprites2D associer au autre label que celui actuel
    private void HideSprite()
    {
        foreach (Node node in GetNode<Node>("Container/VBoxContainerCenter/HBoxContainer").GetChildren())
        {
            if (node is Label label)
            {
                foreach (Node node2 in label.GetChildren())
                {
                    if (node2 is Sprite2D sprite2D)
                    {
                        sprite2D.Visible = false;
                    }
                }
            }
        }
    }


    //Verifie si les lettres des labels sont les memes que le code secret
    private bool CheckCode()
    {
        string code = "";
        foreach (Label label in labelList)
        {
            code += label.Text;
        }
        if (code == SecretCode.ToUpper())
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void lettreSuivante()
    {
        if (alphabet.IndexOf(currentLetter) == alphabet.Count - 1)
        {
            currentLetter = alphabet[0];
        }
        else
        {
            currentLetter = alphabet[alphabet.IndexOf(currentLetter) + 1];
        }
        labelList[currentLabelIndex].Text = currentLetter.ToString();
        PlayAnimation("top");
    }

    private void lettrePrecedente()
    {
        if (alphabet.IndexOf(currentLetter) == 0)
        {
            currentLetter = alphabet[alphabet.Count - 1];
        }
        else
        {
            currentLetter = alphabet[alphabet.IndexOf(currentLetter) - 1];
        }
        labelList[currentLabelIndex].Text = currentLetter.ToString();
        PlayAnimation("bot");
    }

    private void labelSuivant()
    {
        if (currentLabelIndex == labelList.Count - 1)
        {
            currentLabelIndex = 0;
            currentLetter = labelList[currentLabelIndex].Text[0];
        }
        else
        {
            currentLabelIndex++;
            currentLetter = labelList[currentLabelIndex].Text[0];
        }
        ShowSprite();
    }

    private void labelPrecedent()
    {
        if (currentLabelIndex == 0)
        {
            currentLabelIndex = labelList.Count - 1;
            currentLetter = labelList[currentLabelIndex].Text[0];
        }
        else
        {
            currentLabelIndex--;
            currentLetter = labelList[currentLabelIndex].Text[0];
        }
        ShowSprite();
    }


}
