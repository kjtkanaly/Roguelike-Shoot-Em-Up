using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class PlayerMovementDirector : MovementDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerMovementData playerData;
	private Vector2 inputDirection;
	// Public

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		playerData = base.SetPlayerData();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		// Update Velocity Snapshot Variables
		lateralVelocitySnapshot = new Vector2(Velocity.X, 
											  Velocity.Z);
		verticalVelocitySnapshot = Velocity.Y;

		// Apply Vertical Velocity M A T H & Logic
		HandleJump(playerData.jumpVelocity);

		// Apply Laterial Velocity Logic
		HandleBasicLateralMovement((float)delta);
		HandleDodgeRoll((float)delta);

		Velocity = new Vector3(lateralVelocitySnapshot.X, 
							   verticalVelocitySnapshot, 
							   lateralVelocitySnapshot.Y);

		MoveAndSlide();
		GD.Print($"Player Movement Director");
	}

	//-------------------------------------------------------------------------
	// Methods
	private void HandleJump(float jumpVelocity) {
		if (Input.IsActionJustPressed("Jump") && IsOnFloor()) {
			verticalVelocitySnapshot = jumpVelocity;
			GD.Print("Test Jump");
		}
	}

	private void HandleBasicLateralMovement(float delta) {
		Vector3 direction = GetGlobalInputDirectionNorm();

		if (direction != Vector3.Zero) {
			lateralVelocitySnapshot.X = Mathf.MoveToward(
				lateralVelocitySnapshot.X, 
				playerData.speed * direction.X, 
				playerData.acceleration * delta);

			lateralVelocitySnapshot.Y = Mathf.MoveToward(
				lateralVelocitySnapshot.Y,
				playerData.speed * direction.Z,
				playerData.acceleration * delta);

			lateralVelocitySnapshot = GeneralStatic.MagnitudeClamp(
				lateralVelocitySnapshot, 
				playerData.speed);
		}
		else {
			lateralVelocitySnapshot = lateralVelocitySnapshot.MoveToward(
				Vector2.Zero,
				playerData.friction * delta);
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
									  * playerData.rollSpeed;
			PAD.PlayRollAnimation(); // To Do: Change to a signals that the object will emit
		}*/
	}

	private Vector3 GetGlobalInputDirectionNorm() {
		inputDirection = Input.GetVector("Left", "Right", "Up", "Down");
		Vector3 direction = (Transform.Basis 
							 * new Vector3(inputDirection.X, 0, inputDirection.Y));
		direction = direction.Normalized();
		return direction;
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
