// using Godot;
// using System;

// public partial class PickupItem : RigidBody2D
// {
// 	private bool isPickedUp = false;

// 	public void Pickup()
// 	{
// 		isPickedUp = true;
// 		SetDeferred("mode", 0); // Disable physics while picked up (0 for Kinematic)
// 		var player = GetParent().GetNode<player>("Player");
// 		GlobalPosition = player.GlobalPosition; // Follow the player
// 	}

// 	public void Throw(Vector2 direction, float force)
// 	{
// 		isPickedUp = false;
// 		SetDeferred("mode", 2); // Enable physics updates (2 for Rigid)
// 		ApplyImpulse(Vector2.Right * force, Vector2.Zero);

// 	}

// 	// public void OnBodyEntered(Node body)
// 	// {
// 	// 	if (body is Player player)
// 	// 	{
// 	// 		player.PickupItem(this);
// 	// 	}
// 	// }
// 	private void _on_area_2d_body_entered(Node2D body)
// 	{
// 		if (body is player player)
// 		{
// 			player.PickupItem(this);
// 		}
// 	}
// }


