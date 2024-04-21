using Godot;
using System;

public partial class MovementDirector : CharacterBody3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Private
    private float gravity = ProjectSettings.GetSetting(
						   "physics/3d/default_gravity").AsSingle();
    // Public
    // Protected 
    protected Vector2 lateralVelocitySnapshot;
    protected float verticalVelocitySnapshot;
    [Export] protected MovementData movementData;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _PhysicsProcess(double delta)
    {
        // Update Velocity Snapshot Variables
		lateralVelocitySnapshot = new Vector2(Velocity.X, 
											  Velocity.Z);
		verticalVelocitySnapshot = Velocity.Y;

        ApplyGravity((float)delta);

        MoveAndSlide();
    }

    //-------------------------------------------------------------------------
    // Methods
    protected PlayerMovementData SetPlayerData() {
        return (PlayerMovementData) movementData;
    }

    protected void ApplyGravity(float timeDelta) {
		if (!IsOnFloor())
		    verticalVelocitySnapshot -= movementData.mass * gravity * timeDelta;
	}

    //-------------------------------------------------------------------------
	// Debug Methods
}