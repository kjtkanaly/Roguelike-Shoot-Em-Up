using Godot;
using System;

public partial class EnemyMovementDirector : NPCMovementDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	private EnemyMovementData movementData;
	private EnemeyInteractionDirector interactionDir;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		interactionDir = GetNode<EnemeyInteractionDirector>("Enemy-Interaction-Director");
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
	}

	public override void _Process(double delta)
	{
		CheckIfPlayerIsAttackRange();
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public override void LoadMovementkData() {
		movementData = (EnemyMovementData) GD.Load(movementDataPath);
	}

	public override float GetMass() {
		return movementData.mass;
	}

	// Protected

	// Private
	private void CheckIfPlayerIsAttackRange() {

	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
