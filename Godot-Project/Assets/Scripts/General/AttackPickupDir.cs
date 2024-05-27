using Godot;
using System;

public partial class AttackPickupDir : Area3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public PackedScene attackObject;
	
	// Protected
	[Export] protected float idleFrequency = 1.0f;
	[Export] protected float idleAmplitude = 0.01f;
	protected Vector3 pos;

	// Private
	private float time = 0.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		time += (float) delta;
		IdleAnimation(time);
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public

	// Protected
	protected virtual void IdleAnimation(float time) {
		pos = Position;
		pos.Y += idleAmplitude * MathF.Sin(2 * Mathf.Pi * idleFrequency * time);
		Position = pos;
	}

	// Private


	//-------------------------------------------------------------------------
	// Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TO DO: Do some shit
