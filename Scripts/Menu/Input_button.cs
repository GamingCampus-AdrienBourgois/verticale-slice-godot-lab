using Godot;
using System;

public partial class Input_button : Button
{
	[Signal]
	public delegate void ButtonPressedEventHandler();
	public string action;
	private void _on_pressed()
	{
		GD.Print("PRESSEDD");
		EmitSignal("ButtonPressed",this,action);
	}
	public void Emit(){
		EmitSignal("ButtonPressed",this,action);
	}
}




