using Godot;
using System;

public partial class Run : State
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float speedModifier = 1.0f;

    // Protected

    // Private
    [Export] private State idleState;
    [Export] private State jumpState;
    [Export] private State fallState;
    private Vector2 lateralDirection;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    override public State ProcessInput(InputEvent inputEvent) {
        // Trigger Jump Logic here
        if (inputEvent.IsActionPressed("Jump") && characterDir.IsOnFloor()) {
            return jumpState;
        }
        return null;
    }

    override public State ProcessPhysics(float delta) {        
        Vector2 latVelocity = new Vector2(
                characterDir.Velocity.X, 
                characterDir.Velocity.Z);

        // Set the goal speed for the lateral movement
        float goalSpeed = 0;
        if (IsMovingLaterally()) {
            goalSpeed = characterDir.GetMovementData().speed * speedModifier;
        }

        // Get the update speed by moving the current speed towards the goal
        float currentSpeed = latVelocity.Length();
        float updateSpeed = Mathf.MoveToward(
                currentSpeed, 
                goalSpeed, 
                characterDir.GetMovementData().acceleration * delta);

        // Set the latVelocity to have magntiude of the update speed times speedmodifier
        latVelocity = GetLateralDirectionVector() * updateSpeed * speedModifier;

        characterDir.Velocity = new Vector3(
                latVelocity.X, 
                characterDir.Velocity.Y, 
                latVelocity.Y);

        // Rotate the model towards the velocity direction
        float angle = Vector2.Down.AngleTo(latVelocity);
        RotateModelTowardsTarget(angle);

        // Move the character body around
        characterDir.MoveAndSlide();

        // Check if the player is now character is now falling due to their movement
        if (!characterDir.IsOnFloor()) {
            return fallState;
        }

        // Check if the player is now idle
        if (latVelocity.Length() <= 0.01) {
            return idleState;
        }
        
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
