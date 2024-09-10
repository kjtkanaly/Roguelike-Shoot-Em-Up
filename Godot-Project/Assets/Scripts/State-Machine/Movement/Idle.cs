using Godot;
using System;

public partial class Idle : CharacterBodyState
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
    public override void Enter()
    {
        base.Enter();

        characterDir.Velocity = new Vector3(0, characterDir.Velocity.Y, 0);
    }

    override public State ProcessInput(InputEvent inputEvent) {
        // Check if the run state is triggered
        if (IsMovingLaterally() && characterDir.IsOnFloor()) {
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
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
