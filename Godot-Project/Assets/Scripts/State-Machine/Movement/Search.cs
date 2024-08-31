using Godot;
using System;

public partial class Search : NPCState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State chaseState;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public

    // Protected
    public override State ProcessPhysics(float delta)
    {
        if (IsPlayerInChaseRange()) {
            return chaseState;
        }

        return null;
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
