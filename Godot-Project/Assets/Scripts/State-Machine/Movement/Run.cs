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
        LateralMovement(delta, speedModifier);

        // Check if the player is now character is now falling due to their movement
        if (!characterDir.IsOnFloor()) {
            return fallState;
        }

        if (characterDir.Velocity.Z == 0) {
            return idleState;
        }
        
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
