using Godot;
using System;
using System.Collections.Generic;

public partial class Global : Node
{	
	public List<bool> List_Level_Bools = new List<bool>();
	public List<string> List_Level_Texts = new List<string>();
	public string TpOut;
	public string TpEnd;
	public string Message = "";
	public int LevelFinished;
}
