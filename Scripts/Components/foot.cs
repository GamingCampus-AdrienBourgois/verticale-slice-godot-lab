using Godot;
using System;

public partial class foot : StaticBody2D
{
	public int score = 0;

	[Export]
	public PackedScene particule_scene;
	private void _on_area_body_entered(Node2D body)
	{
		if (body.IsInGroup("Ballon")){
			score ++;
			GD.Print("Marqué");
			GD.Print(score);

			// Instantiate the particle scene
			GpuParticles2D particule = (GpuParticles2D)particule_scene.Instantiate();

			// Set the particle position relative to the foot node
			particule.Position = body.GlobalPosition - GlobalPosition;

			// Add the particle as a child of the foot node
			AddChild(particule);

			// Start emitting particles
			particule.Emitting = true;

			// Free the ballon node
			body.QueueFree();
		}
	}
	

}


