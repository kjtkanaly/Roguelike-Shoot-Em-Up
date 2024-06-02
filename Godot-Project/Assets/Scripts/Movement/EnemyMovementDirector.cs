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

		string intDirPath = "Enemy-Interaction-Director/Generic-Interaction-Director";
		interactionDir = GetNode<EnemeyInteractionDirector>(intDirPath);
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
	public override void LoadMovementData() {
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
