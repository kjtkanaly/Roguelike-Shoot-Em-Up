using Godot;
using System;

public partial class Idle : State
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State runState;
    [Export] private State jumpState;
    [Export] private State fallState;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    override public State ProcessInput(InputEvent inputEvent) {
        // Check if the run state is triggered
        if (IsMovingLaterally(inputEvent) && characterDir.IsOnFloor()) {
            return runState;
        }
        // Check if the jump state is triggered
        if (inputEvent.IsActionPressed("Jump") && characterDir.IsOnFloor()) {
            return jumpState;
        }

        return null;
    }

    public override State ProcessPhysics(float delta)
    {
        // Check if the character should now be falling
        if (!characterDir.IsOnFloor()) {
            return fallState;
        }
        characterDir.MoveAndSlide();
        return null;
    }

    // Protected

    // Private
    private bool IsMovingLaterally(InputEvent inputEvent) {
        if (inputEvent.IsAction("Left") 
            || inputEvent.IsAction("Right")
            || inputEvent.IsAction("Down")
            || inputEvent.IsAction("Up")) {
            return true;
        }
        return false;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
