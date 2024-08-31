using Godot;
using System;

public partial class Fall : State
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State idleState;
    [Export] private State runState;
    [Export] private State jumpState;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override State ProcessInput(InputEvent inputEvent)
    {
        return null;
    }

    public override State ProcessPhysics(float delta)
    {
        // Update the character's effects from gravity
        characterDir.ApplyGravity(delta);
        characterDir.MoveAndSlide();

        if (!characterDir.IsOnFloor()) {
            return null;
        }

        if (IsMovingLaterally()) {    
            return runState;
        }
        else { 
            return idleState;
        }
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
