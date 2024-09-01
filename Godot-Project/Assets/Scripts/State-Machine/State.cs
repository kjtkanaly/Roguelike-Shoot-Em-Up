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

    protected void OrientateTowardsTarget(Vector2 target) {
		if (target == Vector2.Zero) {
            return;
        }

        // 1
        float angle = characterDir.Rotation.Y;

        // 2 
        Vector2 topDownNormal = new Vector2(
                characterDir.GlobalTransform.Basis.Z.X, 
                characterDir.GlobalTransform.Basis.Z.Z);
        float diff = topDownNormal.AngleTo(target);

        GD.Print($"OG: {angle} | Diff: {diff}");

        // 3 
        float newAngle = Mathf.MoveToward(
                angle, 
                angle - diff, 
                Mathf.Pi / 16);

        characterDir.Rotation = new Vector3(
                characterDir.Rotation.X, 
                newAngle, 
                characterDir.Rotation.Z);

        // GD.Print($"Prev: {angle} | Goal: {angle - diff} | New Angle: {newAngle}");
    }

    protected void LateralMovement(float delta, float speedModifier) {
        // Get the character's directional inputs
        OrientateTowardsTarget(GetLateralDirectionVector());

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
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
