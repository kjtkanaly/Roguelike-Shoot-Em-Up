using Godot;
using System;

public partial class CharacterBodyState : State
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float moveSpeed;

    // Protected
    protected CharacterDirector characterDir;

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    override public void Init(CharacterDirector bodyDirectorRef, AnimationPlayer animationRef) 
    {
        base.Init(bodyDirectorRef, animationRef);
        characterDir = bodyDirectorRef;
    }

    // Protected
    protected bool IsMovingLaterally() {
        if (Input.IsActionPressed("Left") 
            || Input.IsActionPressed("Right")
            || Input.IsActionPressed("Down")
            || Input.IsActionPressed("Up")) {
            return true;
        }
        return false;        
    }

    protected Vector2 GetLateralDirectionVector() {
        Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
        return direction;
    }

    protected void RotateModelTowardsTarget(float angle) {
		// Face the model towards the direction
        characterDir.GetModel().Rotation = new Vector3(
            characterDir.GetModel().Rotation.X,
            characterDir.Rotation.Y - angle,
            characterDir.GetModel().Rotation.Z);
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}