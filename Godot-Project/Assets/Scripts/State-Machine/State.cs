using Godot;
using System;

public partial class State : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public String animationPath;
    public float moveSpeed;

    // Protected
    protected CharacterDirector characterDir;
    protected AnimationPlayer animationPlayer;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    virtual public void Init(CharacterDirector characterDirRef, AnimationPlayer animationPlayerRef) {
        characterDir = characterDirRef;
        animationPlayer = animationPlayerRef;
    }

    virtual public void Enter() {
        if (!string.IsNullOrEmpty(animationPath)) {
            animationPlayer.Play(animationPath);
        }
        return;
    }

    virtual public void Exit() {
        return;
    }

    virtual public State ProcessInput(InputEvent inputEvent) {
        return null;
    }

    virtual public State ProcessPhysics(float delta) {
        return null;
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
