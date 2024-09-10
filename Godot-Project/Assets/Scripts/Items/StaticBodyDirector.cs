using Godot;
using System;

public partial class StaticBodyDirector : StaticBody3D
{
    public enum ItemType 
    {
        Candy = 0,
        Attack = 1,
        Unkown = -1
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public ItemType itemType = ItemType.Unkown;

    // Protected
    [Export] protected StateMachine stateMachine;
    [Export] protected AnimationPlayer animationPlayer;
    [Export] protected PlayerDirector playerDir;

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public override void _Ready()
    {
        Init();
    }

    public void Init() {
        // Initialize the movement State Machine
        stateMachine.Init(this, animationPlayer);

        FindPlayerDirInScene();
    }

    public override void _PhysicsProcess(double delta)
    {   
        // Call the Movement State Machine's Physics Process
        stateMachine.PhysicsProcess((float) delta);
    }

    public PlayerDirector GetPlayerDir() 
    {
        return playerDir;
    }

    public void FindPlayerDirInScene() {
        foreach (Node node in GetTree().GetNodesInGroup("Player")){
            if (node.IsInGroup("Director")) {
                playerDir = (PlayerDirector) node;
                break;
            }
        }
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
