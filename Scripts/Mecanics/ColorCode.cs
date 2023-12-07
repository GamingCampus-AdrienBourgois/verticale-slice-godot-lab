using Godot;
using System;

public partial class ColorCode : Node
{
	//private int[] code = {1,6,5,1,2,1,4};
	private int[] code = {1,6,5};//code test 
	private bool codeValid = false;

	private colored_computer[] computerList = new colored_computer[3];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int numberPc = 0;
		foreach (colored_computer i in GetChildren())
		{
			computerList[numberPc] = i;
			numberPc++;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void CheckNewCode()
	{
		for(int i = 0; i<computerList.Length; i++)
		{
			if(code[i] != computerList[i].colorStatus)
			{
				codeValid = false;
				break;
			}
			else
			{
				codeValid = true;
			}
		}
		if(codeValid)
		{
			GD.Print("code bon");
		}
	}
}
