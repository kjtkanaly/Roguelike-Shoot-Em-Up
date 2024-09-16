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
    [Export] private StaticBodyState idleItem;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public override State ProcessPhysics(float delta) 
    {
        Vector3 playerPos = staticBodyDir.GetCharacterDir().Position;
        Vector3 newPos = staticBodyDir.Position.MoveToward(playerPos, delta * moveSpeed);
        staticBodyDir.Position = newPos;

        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
