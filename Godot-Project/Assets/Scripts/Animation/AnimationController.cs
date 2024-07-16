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

        if (GetMovementDirector().GetLateralVelocityMag() > 0.1f) {
            StartWalkRunAnimation();
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
        animationPlyr = GetNode<AnimationPlayer>("Animation_Player");
    }

    protected virtual void SetMovementDirector() {
        movementDir = GetNode<MovementDirector>("..");
    }

    protected virtual MovementDirector GetMovementDirector() {
        return movementDir;
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
