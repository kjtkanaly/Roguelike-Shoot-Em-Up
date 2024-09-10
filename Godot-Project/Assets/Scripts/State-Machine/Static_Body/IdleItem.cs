using Godot;
using System;

public partial class IdleItem : StaticBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] float freq = 1.0f;
    [Export] float followRange = 5.0f;

    // Protected
    [Export] protected State followPlayer;
    protected float yIntercept = 0.5f;
    protected float time = 0.0f;

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public override void Enter()
    {
        base.Enter();

        yIntercept = staticBodyDir.Position.Y;
    }

    override public State ProcessPhysics(float delta) 
    {
        Vector3 newPos = staticBodyDir.Position;
        time += delta;
        
        newPos.Y = EaseInOutSine(time, freq, yIntercept);
        staticBodyDir.Position = newPos;

        // Check if the item is within follow range of the player
        if (IsInFollowRange()) {
            return followPlayer;
        }

        return null;
    }

    // Protected
    protected bool IsInFollowRange() {
        if (staticBodyDir.GetPlayerDir() == null) {
            return false;
        }

        Vector3 playerPos = staticBodyDir.GetPlayerDir().Position;
        Vector3 thisPos = staticBodyDir.Position;

        if (playerPos.DistanceTo(thisPos) < followRange) {
            return true;
        }

        return false;
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
