using Godot;
using System;

public partial class NPCMovementDirector : MovementDirector
{
	public enum Mode {
		Idle = 0,
		Active = 1,
		Attack = 2,
		FollowPlayer = 3
	}
	
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	public Mode mode = Mode.Idle;

	// Protected

	// Private
	private NPCMovementData movementData;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
	}

	public override void _Process(double delta)
	{
		
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public override void LoadMovementData() {
		movementData = (NPCMovementData) GD.Load(movementDataPath);
	}

	public override float GetMass() {
		return movementData.mass;
	}

	// Protected

	// Private

	//-------------------------------------------------------------------------
	// Debug Methods
}
