using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private CharacterDirector playerDir;
    private State currentState;
    [Export] private State startingState;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init(CharacterDirector characterDirRef, AnimationPlayer animationRef) {
        // Assign the player director reference
        playerDir = characterDirRef;

        // Init all of the child state objects
        foreach (State child in GetChildren()) {
            child.Init(playerDir, animationRef);
        }

        // Initialize to the default state
        ChangeState(startingState);
    }

    public void ChangeState(State newState) {
        // If there is a current state, call any exit logic
        if (currentState != null) {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void PhysicsProcess(double delta)
    {

    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
