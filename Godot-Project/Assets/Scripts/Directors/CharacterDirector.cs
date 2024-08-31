using Godot;
using System;

public partial class CharacterDirector : CharacterBody3D
{
    public enum CharacterType {
        Player = 0,
        Ally = 1,
        Enemy = 2
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public String movementDataPath;
    [Export] public CharacterType charactertype;

    // Protected
    [Export] protected AnimationPlayer animations;
    [Export] protected StateMachine movementSM;
    protected MovementData movementData;

    // Private

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        // Ready the base class
        base._Ready();

        // Get the memeber nodes
        movementData = (MovementData) GD.Load(movementDataPath);

        // Initialize the movement State Machine
        movementSM.Init(this, animations);
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        movementSM.ProcessInput(inputEvent);
    }

    public override void _PhysicsProcess(double delta)
    {   
        // Call the Movement State Machine's Physics Process
        movementSM.PhysicsProcess((float) delta);
    }

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public MovementData GetMovementData() {
        return movementData;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
