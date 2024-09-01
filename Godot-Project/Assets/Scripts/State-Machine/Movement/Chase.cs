using Godot;
using System;

public partial class Chase : NPCState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State searchState;    

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override State ProcessPhysics(float delta)
    {
        // Get Unit Vecotr to Player
        Vector2 direction = GetLateralDirectionToPlayer();

        // Have the NPC face the player
        OrientateTowardsTarget(direction);

        // Set the velocity towards the player
        Vector2 velocity = characterDir.GetMovementData().speed * direction;

        characterDir.Velocity = new Vector3(
                velocity.X,
                characterDir.Velocity.Y,
                velocity.Y);

        characterDir.MoveAndSlide();
        
        if (!IsPlayerInChaseRange()) {
            return searchState;
        }

        return null;
    }

    // Protected

    

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
