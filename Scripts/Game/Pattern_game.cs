using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;

public partial class Pattern_game : Control
{
	Node vbox = null;
	PackedScene hbox_scene = null;
	ColorRect colorRect_scene = null;

	int height = 9;
	int width = 20;
	// PackedScene button_input = ResourceLoader.Load("res://Scenes/Menu/Input_button.tscn") as PackedScene;


	Godot.Vector2 selected = new Godot.Vector2(0,0);
	List<Godot.Vector2> ToSelect = new List<Godot.Vector2>();

	List<Godot.Vector2> Found = new List<Godot.Vector2>();

	public override void _Ready()
	{
		Random rnd = new Random();
		for (int i = 0; i < width/10; i++)
		{
			ToSelect.Add(new Godot.Vector2(rnd.Next(0,height-1),rnd.Next(0,width-1)));
			GD.Print(ToSelect[i]);
		}
		// ToSelect.Add(new Godot.Vector2(2,2));
		// ToSelect.Add(new Godot.Vector2(1,3));
		
		vbox = GetNode("Vbox");

		for (int i = 0; i < height; i++)
		{
			var Hbox = new HBoxContainer();
			vbox.AddChild(Hbox);
		}
		foreach(Node x in vbox.GetChildren())
		{
			for (int i = 0; i < width; i++)
			{
				var colorRect = new ColorRect
				{
					CustomMinimumSize = new Godot.Vector2(50, 50)
				};
				x.AddChild(colorRect);
			}
		}
		ToColor(selected,new Color(158,177,228));
	}

	public override void _Input(InputEvent @event)
	{
		bool Confirm = true;
		if(Found.Count() == ToSelect.Count())
		{
			for (int i = 0; i < Found.Count()-1; i++)
			{
				if(ToSelect[i] != Found[i])
				{
					Confirm = false;
					break;
				}
			}
			if (Confirm == true)
			{
				GD.Print("Resolved");
				// Envoie sur la salle dans l'espace avec le mec ou dans la room
			}
		}
		
		Godot.Vector2 AncientSelec = selected;
		// Mettre que si il appuie sur une touche ça enlève le rouge
		if(@event.IsActionPressed("Up")){selected.X -= 1;}
		else if(@event.IsActionPressed("Down")){selected.X += 1;}
		else if(@event.IsActionPressed("Right")){selected.Y += 1;}
		else if(@event.IsActionPressed("Left")){selected.Y -= 1;}
		if(selected.X < 0 || selected.Y < 0 || selected.X > height-1 || selected.Y > width-1)
		{
			selected = AncientSelec;
		}

		ToColor(AncientSelec, new Color(255,255,255));
		if (Found.Count() > 0)
		{
			for (int i = 0; i < Found.Count(); i++)
			{
				ToColor(Found[i],new Color(255,0,0));
			}
		}
		ToColor(selected,new Color(0,0,255));

		

		// GD.Print("selected : ",selected);
		if (@event.IsActionPressed("Accept"))
		{
			GD.Print("OK");
			ToColor(selected,new Color(255,0,0));
			if(!Found.Contains(selected)){
				Found.Add(selected);
			}
		}
	}


	private void ToColor(Godot.Vector2 thing, Color color)
	{
		var hbox_to = vbox.GetChild((int)thing.X);
		var childHbox = hbox_to.GetChild((int)thing.Y);
		ColorRect temp = (ColorRect)childHbox;
		temp.Color = color;
	}


}
