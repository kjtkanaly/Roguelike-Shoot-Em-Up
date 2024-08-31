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
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
