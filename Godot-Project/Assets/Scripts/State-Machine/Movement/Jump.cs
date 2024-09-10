using Godot;
using System;

public partial class Jump : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State fallState;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        base.Enter();

        // Set the Jump Velocity
        characterDir.Velocity = new Vector3(
                characterDir.Velocity.X, 
                characterDir.GetMovementData().jumpVelocity, 
                characterDir.Velocity.Z);

        characterDir.MoveAndSlide();
    }

    public override State ProcessPhysics(float delta)
    {
        if (!characterDir.IsOnFloor()) {
            characterDir.ApplyGravity(delta);
        }

        characterDir.MoveAndSlide();

        if (characterDir.Velocity.Y <= 0) {
            return fallState;
        }

        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}