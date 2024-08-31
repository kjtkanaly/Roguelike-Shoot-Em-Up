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

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override State ProcessPhysics(float delta)
    {
        float verticalSpeed = characterDir.Velocity.Y;
		if (!characterDir.IsOnFloor()) {
			verticalSpeed -= characterDir.GetMovementData().mass  * gravity  * delta;
			characterDir.Velocity = new Vector3(
                    characterDir.Velocity.X, 
                    verticalSpeed, 
                    characterDir.Velocity.Z);
        } 
        else {
            return idleState;
        }
        characterDir.MoveAndSlide();
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}