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
        // Get the character's directional inputs
        OrientateBody();

        float goalSpeed;
        if (IsMovingLaterally()) {
            goalSpeed = characterDir.GetMovementData().speed * speedModifier;
        } else {
            goalSpeed = 0;
        }

        // Move the character's current closer to the goal speed 
        float currentSpeed = Mathf.MoveToward(
                characterDir.Velocity.Z, 
                goalSpeed, 
                characterDir.GetMovementData().acceleration * delta);
        
        // Calcualte the character's lateral 2-D Velocity 
        Vector2 lateralVelocity =  
                new Vector2(Mathf.Sin(characterDir.Rotation.Y), 
                            Mathf.Cos(characterDir.Rotation.Y))
                * currentSpeed;

        // Update the character body's Velocity
        characterDir.Velocity = new Vector3(
                lateralVelocity.X, 
                characterDir.Velocity.Y, 
                lateralVelocity.Y);

        // Move the character body around
        characterDir.MoveAndSlide();

        // Check if the player is now character is now falling due to their movement
        if (!characterDir.IsOnFloor()) {
            return fallState;
        }

        if (currentSpeed == 0) {
            return idleState;
        }
        
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
