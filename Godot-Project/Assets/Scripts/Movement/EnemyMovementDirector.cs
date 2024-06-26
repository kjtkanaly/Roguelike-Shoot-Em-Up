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
	private Node3D playerNode;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		string intDirPath = "Enemy-Interaction-Director/Generic-Interaction-Director";
		interactionDir = GetNode<EnemeyInteractionDirector>(intDirPath);

		playerNode = GetTree().Root.GetChild(0).GetNode<Node3D>("Player-Director");
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		FollowPlayer();
		MoveAndSlide();
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

	private void FollowPlayer() {
		Vector2 playerPos = new Vector2(playerNode.Position.X, 
										playerNode.Position.Z);
		Vector2 thisPos = new Vector2(GlobalPosition.X, GlobalPosition.Z); 
		Vector2 xzVelocity = (playerPos - thisPos).Normalized() * movementData.speed;
		Velocity = new Vector3(xzVelocity.X, Velocity.Y, xzVelocity.Y);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
