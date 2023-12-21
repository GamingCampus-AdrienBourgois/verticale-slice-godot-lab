using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class DoWhatHeSays : Control
{
	[Export]
	int height = 5;
	[Export]
	int width = 8;
	Node vbox = null;
	Vector2 selected = new Vector2(0,0);
	private int Did = 0;
	private bool Action = false;
	private int Played = 0;

	// Un jeu qui fait apparaitre une direction et il faut la faire
	
	// SOIT
	// Player se déplace pas : Vitesse direction (jeu dans le mii party, avec la gymnastique et faut immiter)
	// Player peut se déplacer: Des walls spawnent pour augmenter la difficulté

	// Quand il fait un input si il est dans la direction

	private List<string> Movements = new List<string>();
	private List<string> MovementsAll = new List<string>{"Up","Down","Left","Right"};
	private Label label = null;
	private Label label_niveau = null;
	private bool FailedPattern = false;
	private bool FailedMovement = false;
	private Godot.Timer timer = null;

	public override void _Ready()
	{
		label_niveau = GetNode<Label>("Label2");
		label = GetNode<Label>("Label");
		GD.Print("Ready");
		Init();
		CreatePattern();
	}

	private void Init()
	{
		// AddChild(new Label
		// {
		// 	Name = "Label",
		// 	AnchorsPreset = 0,
			
		// });
		AddChild(new Godot.Timer
		{
			Name = "TimerMovement",
			WaitTime = 2,
			OneShot = true,
			Autostart = false
		});
		timer = GetNode<Godot.Timer>("TimerMovement");
		timer.Connect(Godot.Timer.SignalName.Timeout, new Callable(this,"OnTimeout"));
	}

	private void CreatePattern()
	{
		Random rnd = new Random();
		FailedPattern = false;
		Movements.Clear();
		Played = 0;
		for(int i = 0; i < Did+1; i++)
		{
			Movements.Add(MovementsAll[rnd.Next(0,3)]);
		}		
		DoPattern();
		label_niveau.Text = "Niveau : "+Did;
	}
	private async void DoPattern()
	{
		for(int i = 0; i < Movements.Count; i++)
		{
			// Faire un timer, si timeout alors, failedmovement = true
			label.Text = Movements[Played];
			Action = true;
			timer.Start();
			await ToSignal(GetTree().CreateTimer(timer.WaitTime),"timeout");
			//await ToSignal(timer,Godot.Timer.SignalName.Timeout);
			Action = false;
			if(FailedMovement)
			{
				GD.Print("Failed");
				FailedMovement = false;
			}
			// else
			// {

			// }
			await ToSignal(GetTree().CreateTimer(1),"timeout");
			if(Played >= Movements.Count-1)
			{
				Played = 0;
				label.Text = "Finished level:"+ Did;
				Did++;
				await ToSignal(GetTree().CreateTimer(1),"timeout");
				CreatePattern();
			}
			// Play animation
			// Wait till end
			// Turn the value to false, so no more inputs, checks if he did something
		}
	}

	private async void OnTimeout()
	{
		//GD.Print("HELLO");
		FailedMovement = true;
		await ToSignal(GetTree().CreateTimer(1),"timeout");
		Did = 0;  
		CreatePattern();
	}

    public override void _Input(InputEvent @event)
    {
		if(Action)
		{
			//GD.Print(@event.AsText());
			if(@event.IsActionPressed(MovementsAll[0]) || @event.IsActionPressed(MovementsAll[1]) || @event.IsActionPressed(MovementsAll[2]) || @event.IsActionPressed(MovementsAll[3]) )
			{
				Action = false;
        		if(@event.IsActionPressed(Movements[Played]))
				{
					GD.Print("Good");
					label.Text = "Good";
					timer.Stop();
					Played++;
				}
				else
				{
					GD.Print("Wrong");
					FailedMovement = true;
					label.Text = "Wrong";
				}
			}
			else 
			{
				GD.Print("Other input");
			}
		}
    }
}
