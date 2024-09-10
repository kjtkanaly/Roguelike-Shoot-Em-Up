using Godot;
using System;

public partial class FollowPlayer : StaticBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public float moveSpeed = 1.0f;

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    override public State ProcessPhysics(float delta) 
    {
        Vector3 playerPos = staticBodyDir.GetPlayerDir().Position;
        Vector3 newPos = staticBodyDir.Position.MoveToward(playerPos, delta * moveSpeed);
        staticBodyDir.Position = newPos;

        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
