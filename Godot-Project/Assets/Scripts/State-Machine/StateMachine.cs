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
    private State currentState;
    [Export] private State startingState;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    // Init the Character Body State Machine
    public void Init(CharacterDirector objectDirRef, AnimationPlayer animationRef) {
        // Init all of the child state objects
        foreach (State child in GetChildren()) {
            child.Init(objectDirRef, animationRef);
        }

        // Initialize to the default state
        ChangeState(startingState);
    }

    // Init the Staticbody State Machine
    public void Init(StaticBodyDirector objectDirRef, AnimationPlayer animationRef) {
        // Init all of the child state objects
        foreach (State child in GetChildren()) {
            child.Init(objectDirRef, animationRef);
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
