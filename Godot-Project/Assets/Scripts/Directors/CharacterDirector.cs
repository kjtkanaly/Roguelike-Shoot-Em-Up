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
    public float gravity = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");

    // Protected
    [Export] protected AnimationPlayer animations;
    [Export] protected StateMachine movementSM;
    [Export] protected Node3D model;
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

    public Node3D GetModel() {
        return model;
    }

    public void ApplyGravity(float delta) {
        float verticalSpeed = 
                Velocity.Y 
                - (movementData.mass  * gravity  * delta);
        Velocity = new Vector3(
                Velocity.X, 
                verticalSpeed, 
                Velocity.Z);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
