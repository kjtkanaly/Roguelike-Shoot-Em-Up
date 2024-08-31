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
    private CharacterDirector characterDir;
    private State currentState;
    [Export] private State startingState;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init(CharacterDirector characterDirRef, AnimationPlayer animationRef) {
        // Assign the player director reference
        characterDir = characterDirRef;

        // Init all of the child state objects
        foreach (State child in GetChildren()) {
            child.Init(characterDir, animationRef);
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

    public void ProcessInput(InputEvent inputEvent) {
        State newState = currentState.ProcessInput(inputEvent);
        if (newState != null) {
            ChangeState(newState);
        }
    }

    public void PhysicsProcess(float delta)
    {
        State newState = currentState.ProcessPhysics(delta);
        if (newState != null) {
            ChangeState(newState);
        }
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
