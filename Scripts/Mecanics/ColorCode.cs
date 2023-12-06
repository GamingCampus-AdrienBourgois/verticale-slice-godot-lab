using Godot;
using System;

public partial class ColorCode : Node
{
	//private int[] code = {1,6,5,1,2,1,4};
	private int[] code = {1,6,5};//code test 
	private bool codeValid = false;

	// Called when the node enters the scene tree for the first time.
	[Export]
	private colored_computer[] computerList = new colored_computer[3];
	public override void _Ready()
	{
		// computerList[0] = GetNode<colored_computer>("ColoredPc/ColoredPC1");
		// computerList[1] = GetNode<colored_computer>("ColoredPc/ColoredPC2");
		// computerList[2] = GetNode<colored_computer>("ColoredPc/ColoredPC3");
		// computerList[3] = GetNode<colored_computer>("ColoredPc/ColoredPC4");
		// computerList[4] = GetNode<colored_computer>("ColoredPc/ColoredPC5");
		// computerList[5] = GetNode<colored_computer>("ColoredPc/ColoredPC6");
		// computerList[6] = GetNode<colored_computer>("ColoredPc/ColoredPC7");
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
