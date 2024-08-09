using Godot;
using System;

public partial class AttackPickupDir : Area3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	
	// Protected

	// Private
	[Export] private float idleFrequency = 1.0f;
	[Export] private float idleAmplitude = 0.01f;
	[Export] private PackedScene attackObject;
	private string attackName;
	private float time = 0.0f;
	private MeshInstance3D mesh;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		mesh = GetNode<MeshInstance3D>("Mesh");

		attackName = attackObject.Instantiate().Name;
	}

	public override void _Process(double delta)
	{
		time += (float) delta;
		IdleAnimation(time);
	}

    public PackedScene GetAttackPackedScene() {
		return attackObject;
	}

	public string GetAttackName() {
		return attackName;
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public

	// Protected
	protected virtual void IdleAnimation(float time) {
		Vector3 pos = Position;
		pos.Y += idleAmplitude * MathF.Sin(2 * Mathf.Pi * idleFrequency * time);
		Position = pos;
	}

	// Private


	//-------------------------------------------------------------------------
	// Debug Methods
}
