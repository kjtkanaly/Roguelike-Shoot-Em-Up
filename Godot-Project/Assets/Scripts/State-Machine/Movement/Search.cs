using Godot;
using System;

public partial class Search : NPCState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public bool isWandering = false;

    // Protected

    // Private
    [Export] private State chaseState;
    private RandomNumberGenerator rng;
    private SceneTreeTimer wanderTimer;
    private SceneTreeTimer waitTimer;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Init(CharacterDirector characterDirRef, AnimationPlayer animationPlayerRef)
    {
        base.Init(characterDirRef, animationPlayerRef);
        rng = new RandomNumberGenerator();
    }

    public override void Enter()
    {
        base.Enter();
        WanderInRandomDirection();
    }

    public override State ProcessPhysics(float delta)
    {
        if (IsPlayerInChaseRange()) {
            return chaseState;
        }

        if (isWandering && (wanderTimer.TimeLeft <= 0)) {
            WaitForRandomTime();
        }
        else if (!isWandering && (waitTimer.TimeLeft <= 0)){
            WanderInRandomDirection();
        }

        characterDir.MoveAndSlide();

        return null;
    }

    // Protected
    protected void WanderInRandomDirection() {
        // Randomly Choose an angle to wander in
        float angle = rng.RandfRange(-Mathf.Pi, Mathf.Pi);

        // Initialize the Wander Velocity and then rotate it by the wander angle
        Vector2 randomVelocity = new Vector2 (0, 1) 
                                 * characterDir.GetMovementData().speed;
        randomVelocity = randomVelocity.Rotated(angle);
        characterDir.Velocity = new Vector3(
                randomVelocity.X, 
                characterDir.Velocity.Y,
                randomVelocity.Y);

        // Face the model towards the direction
        characterDir.GetModel().Rotation = new Vector3(
            characterDir.GetModel().Rotation.X,
            characterDir.Rotation.Y - angle,
            characterDir.GetModel().Rotation.Z);

        // Start the wandering timer with a random time
        float wanderTime = rng.RandfRange(0.5f, 1.5f);
        wanderTimer = GetTree().CreateTimer(wanderTime);

        // Log that the enetity is wandering
        isWandering = true;
    }

    protected void WaitForRandomTime() {
        // Stop the enentity from wandering anymore
        characterDir.Velocity = new Vector3(
                0,
                characterDir.Velocity.Y,
                0);

        // Choose a random amount of time to wait
        float waitTime = rng.RandfRange(0.5f, 1.5f);
        waitTimer = GetTree().CreateTimer(waitTime);

        // Log that the enetity is NOT wandering
        isWandering = false;
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
