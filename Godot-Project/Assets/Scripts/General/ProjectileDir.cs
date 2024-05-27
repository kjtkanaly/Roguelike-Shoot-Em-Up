using Godot;
using System;

public partial class ProjectileDir : RigidBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private Timer despawnTimer = null;
	private bool debug = false;
	// Public
	public float damage = 0.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		despawnTimer = GetNode<Timer>("Despawn-Timer");
		despawnTimer.Start();
		despawnTimer.Timeout += Despawn;
	}

	//-------------------------------------------------------------------------
	// Methods
	private void Despawn() {
		if (debug) {
			GD.Print("Despawn projectile");
		}

		QueueFree();
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
