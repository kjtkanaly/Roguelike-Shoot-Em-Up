using Godot;
using System;

public partial class CharacterDirector : CharacterBody3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public String animationsPath;
    [Export] public String movementSMPath;
    [Export] public String movementDataPath;

    // Protected
    protected AnimationPlayer animations;
    protected StateMachine movementSM;
    protected MovementData movementData;

    // Private

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        // Ready the base class
        base._Ready();

        // Get the memeber nodes
        animations = GetNode<AnimationPlayer>(animationsPath);
        movementSM = GetNode<StateMachine>(movementSMPath);
        movementData = (MovementData) GD.Load(movementDataPath);

        // Initialize the movement State Machine
        movementSM.Init(this, animations);
    }

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public MovementData GetMovementData() {
        return movementData;
    }

    public override void _PhysicsProcess(double delta)
    {   
        // Call the Movement State Machine's Physics Process
        movementSM.PhysicsProcess(delta);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}