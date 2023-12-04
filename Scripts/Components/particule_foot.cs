using Godot;
using System;

public partial class particule_foot : GpuParticles2D
{
	private void _on_finished()
	{
		QueueFree();
	}

}


