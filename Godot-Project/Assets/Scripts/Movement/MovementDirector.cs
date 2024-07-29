using Godot;
using System;

public partial class MovementDirector : CharacterBody3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public string movementDataPath;
	[Export] public string interactionDirNodePath;
	[Export] public string staggeredTimerPath;

	// Protected 
	protected float speedModifier = 1.0f;
	protected float staggeredSpeedModifier = 0.0f;
	protected Vector2 lateralDirection;
	protected float verticalVelocitySnapshot;
	protected float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();
	protected InteractionDirector interactionDirector;

	// Private
	private MovementData movementData;
	private Timer staggeredTimer;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		LoadMovementData();
		interactionDirector = 
			GetNode<InteractionDirector>(interactionDirNodePath);
		interactionDirector.TookDamage += BeginStaggeredMovement;

		staggeredTimer = GetNode<Timer>(staggeredTimerPath);
        staggeredTimer.Timeout += StopStaggeredMovement;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (interactionDirector.IsDead()) {
			return;
		}

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

	public float GetCurrentSpeed() {
		return GetMovementData().speed * speedModifier;
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
			goalSpeed = GetCurrentSpeed();
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

	protected void BeginStaggeredMovement(AttackData data) {
		speedModifier = staggeredSpeedModifier;
		staggeredTimer.Start(data.staggerTime);
	}

	protected void StopStaggeredMovement() {
		speedModifier = 1.0f;
	}

	// Private
	private void ApplyGravity(float timeDelta) {
		float verticalSpeed = Velocity.Y;
		if (!IsOnFloor())
			verticalSpeed -= GetMovementData().mass  * gravity  * timeDelta;
			Velocity = new Vector3(Velocity.X, verticalSpeed, Velocity.Z);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
