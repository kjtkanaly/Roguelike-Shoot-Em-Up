using Godot;
using System;

public partial class MovementDirector : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public string movementDataPath;

	// Protected 
	protected Vector2 lateralDirection;
	protected float verticalVelocitySnapshot;
	protected float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();

	// Private
	private MovementData movementData;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		LoadMovementData();
	}

	public override void _PhysicsProcess(double delta)
	{
		// Update Velocity Snapshot Variables
		verticalVelocitySnapshot = Velocity.Y;

		ApplyGravity((float) delta);

		UpdateLateralDirection();
		OrientateBody();
		SetLateralVelocity((float) delta);

		MoveAndSlide();
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public virtual void LoadMovementData() {
		movementData = (MovementData) GD.Load(movementDataPath);
	}

	public virtual MovementData GetMovementData() {
		return movementData;
	}

	public float GetLateralVelocityMag() {
		Vector2 lateralVelocity = new Vector2(Velocity.X, Velocity.Z);
		return lateralVelocity.Length();
	}

	// Protected
	protected virtual void UpdateLateralDirection() {
		Vector3 globalDirection = new Vector3(0.0f, 0.0f, 1.0f);
		lateralDirection = new Vector2(globalDirection.X, globalDirection.Z);
	}

	protected void SetLateralVelocity(float delta) {
		float currentSpeed = Velocity.Z;
		float goalSpeed = 0;
		if (lateralDirection.Length() != 0) {
			goalSpeed = GetMovementData().speed;
		}
		currentSpeed = Mathf.MoveToward(
				currentSpeed, 
				goalSpeed, 
				GetMovementData().acceleration * delta);
		Vector2 latVelocity = 
			new Vector2(Mathf.Sin(Rotation.Y), Mathf.Cos(Rotation.Y))
			* currentSpeed;
		Velocity = new Vector3(latVelocity.X, Velocity.Y, latVelocity.Y);
	}

	protected void OrientateBody() {
		if (lateralDirection != Vector2.Zero) {
			float angle = -1 * (lateralDirection.Angle() - Mathf.Pi/2);
			Rotation = new Vector3(Rotation.X, angle, Rotation.Z);
		}
    }

	// Private
	private void ApplyGravity(float timeDelta) {
		if (!IsOnFloor())
			verticalVelocitySnapshot -= GetMovementData().mass 
										* gravity 
										* timeDelta;
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
