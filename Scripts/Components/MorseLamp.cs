using Godot;
using System;
using System.Collections.Generic;

public partial class MorseLamp : Sprite2D
{
		private Dictionary<char, string> alphabetMorse = new Dictionary<char, string>
		{
			{'A', "01"},
			{'B', "1000"},
			{'C', "1010"},
			{'D', "100"},
			{'E', "0"},
			{'F', "0010"},
			{'G', "110"},
			{'H', "0000"},
			{'I', "00"},
			{'J', "0111"},
			{'K', "101"},
			{'L', "0100"},
			{'M', "11"},
			{'N', "10"},
			{'O', "111"},
			{'P', "0110"},
			{'Q', "1101"},
			{'R', "010"},
			{'S', "000"},
			{'T', "1"},
			{'U', "001"},
			{'V', "0001"},
			{'W', "011"},
			{'X', "1001"},
			{'Y', "1011"},
			{'Z', "1100"},
		};

	AnimationPlayer animation = null;
	
	bool boolTest = false;
	private string secretCode;

	public override void _Ready()
	{
		secretCode = GetParent().GetNode<MorseCodeHud>("MorseCodeHud").SecretCode;
		animation = GetNode<AnimationPlayer>("AnimationPlayer");
		MorseCode(secretCode);

	}

	private async void MorseCode(string mot)
	{
		string lettreMorse = "";
		foreach(char lettre in mot.ToUpper())
		{
			//lettre.ToString();
			if(alphabetMorse.TryGetValue(lettre, out string morse))
			{
				lettreMorse = morse;
			}


			foreach (char number in lettreMorse)
			{
				if(number == '1'){
					animation.Play("LongLight");
				}
				else
				{
					animation.Play("ShortLight");
				}
				await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
			}
			animation.Play("LightOff");
			await ToSignal(animation, AnimationPlayer.SignalName.AnimationFinished);
		}

	   
	}
}
