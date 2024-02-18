using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class PlayerMovement : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerMovementData playerData;
	private CharacterBody3D charBody;
	private Vector2 lateralVelocitySnapshot;
	private Vector2 inputDirection;
	private float verticalVelocitySnapshot;
	private float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();
	// Public

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		charBody = GetNode<CharacterBody3D>("../");
		playerData = GetNode<PlayerData>("../Player-Data-Director").movementData;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Update Velocity Snapshot Variables
		lateralVelocitySnapshot = new Vector2(charBody.Velocity.X, 
											  charBody.Velocity.Z);
		verticalVelocitySnapshot = charBody.Velocity.Y;

		// Apply Vertical Velocity M A T H & Logic
		ApplyGravity((float)delta);
		HandleJump(playerData.jumpVelocity);

		// Apply Laterial Velocity Logic
		HandleBasicLateralMovement((float)delta);
		HandleDodgeRoll((float)delta);

		charBody.Velocity = new Vector3(lateralVelocitySnapshot.X, 
							   			verticalVelocitySnapshot, 
							   			lateralVelocitySnapshot.Y);

		charBody.MoveAndSlide();
	}

	//-------------------------------------------------------------------------
	// Methods
	private void ApplyGravity(float timeDelta) {
		if (!charBody.IsOnFloor())
			verticalVelocitySnapshot -= playerData.mass * gravity * timeDelta;
	}

	private void HandleJump(float jumpVelocity) {
		if (Input.IsActionJustPressed("Jump") && charBody.IsOnFloor())
			verticalVelocitySnapshot = jumpVelocity;
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

		if (Input.IsActionPressed("Roll") && charBody.IsOnFloor()) {
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
