using Godot;
using System;

public partial class MovementDirector : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public string movementDataPath;

	// Protected 
	protected Vector2 lateralVelocitySnapshot;
	protected float verticalVelocitySnapshot;
	protected float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();

	// Private
	private MovementData movementData;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		LoadMovementkData();
	}

	public override void _PhysicsProcess(double delta)
	{
		// Update Velocity Snapshot Variables
		lateralVelocitySnapshot = new Vector2(Velocity.X, 
											  Velocity.Z);
		verticalVelocitySnapshot = Velocity.Y;

		ApplyGravity((float) delta);

		Velocity = new Vector3(lateralVelocitySnapshot.X, 
							   verticalVelocitySnapshot, 
							   lateralVelocitySnapshot.Y);

		MoveAndSlide();
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public virtual void LoadMovementkData() {
		movementData = (MovementData) GD.Load(movementDataPath);
	}

	public virtual float GetMass() {
		return movementData.mass;
	}

	// Protected


	// Private
	private void ApplyGravity(float timeDelta) {
		if (!IsOnFloor())
			verticalVelocitySnapshot -= GetMass() * gravity * timeDelta;
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
