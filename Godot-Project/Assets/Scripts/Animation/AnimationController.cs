using Godot;
using System;

public partial class AnimationController : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string idleAnimationString;
    [Export] public string walkRunAnimationString;

    // Protected
    protected AnimationPlayer animationPlyr;

    // Private
    private MovementDirector movementDir;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        SetAnimationPlayer();
        SetMovementDirector();

        StartIdleAnimation();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (GetMovementDirector().GetLateralVelocitySnapshot().Length() > 0.1f) {
            StartWalkRunAnimation();
            OrientateModel();
        } else {
            StartIdleAnimation();
        }

    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void StartWalkRunAnimation() {
        if (animationPlyr.CurrentAnimation == walkRunAnimationString) {
            return;
        }

        animationPlyr.Play(walkRunAnimationString);
    }

    public void StartIdleAnimation() {
        if (animationPlyr.CurrentAnimation == idleAnimationString) {
            return;
        }

        animationPlyr.Play(idleAnimationString);
    }

    // Protected
    protected virtual void SetAnimationPlayer() {
        animationPlyr = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    protected virtual void SetMovementDirector() {
        movementDir = GetNode<MovementDirector>("..");
    }

    protected virtual MovementDirector GetMovementDirector() {
        return movementDir;
    }

    protected void OrientateModel() {
        Vector2 velocity = GetMovementDirector().GetLateralVelocitySnapshot();
        float angle = -1 * (velocity.Angle() - Mathf.Pi/2);
        GD.Print(angle);

        Rotation = new Vector3(Rotation.X, angle, Rotation.Z);
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
