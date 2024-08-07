using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class PlayerMovementDirector : MovementDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	private PlayerMovementData movementData;
	private Vector2 inputDirection;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		// Apply Vertical Velocity Logic
		HandleJump(GetMovementData().jumpVelocity);

		// Apply Laterial Velocity Logic
		HandleDodgeRoll((float)delta);

		MoveAndSlide();
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public override void LoadMovementData() {
		movementData = (PlayerMovementData) GD.Load(movementDataPath);
	}

	public override PlayerMovementData GetMovementData() {
		return movementData;
	}

	// Protected
	protected override void UpdateLateralDirection() {
		lateralDirection = Input.GetVector("Left", "Right", "Up", "Down");
	}

	// Private
	private void HandleJump(float jumpVelocity) {
		if (Input.IsActionJustPressed("Jump") && IsOnFloor()) {
			float verticalSpeed = jumpVelocity;
			Velocity = new Vector3(Velocity.X, verticalSpeed, Velocity.Z);
		}
	}

	private void HandleDodgeRoll(float delta) {
		/*
		// Check if currently rolling
		if (PAD.CheckPlayingStatus() && (PAD.GetCurrentAnimationName() == "Roll"))
			return;

		if (Input.IsActionPressed("Roll") && IsOnFloor()) {
			Vector3 direction = GetGlobalInputDirectionNorm();

			if (direction == Vector3.Zero) {
				Vector3 basis = GlobalTransform.Basis.Z;
				direction = new Vector3(basis.X, 0, basis.Z).Normalized();
			}

			lateralVelocitySnapshot = new Vector2(direction.X, direction.Z) 
									  * movementData.rollSpeed;
			PAD.PlayRollAnimation(); // To Do: Change to a signals that the object will emit
		}*/
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
