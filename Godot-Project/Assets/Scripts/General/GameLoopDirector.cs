using Godot;
using System;

public partial class GameLoopDirector : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string playerInteractionDirNodePath;

    // Protected

    // Private
    private PlayerInteractionDirector playerInteractionDir;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        playerInteractionDir = 
            GetNode<PlayerInteractionDirector>(playerInteractionDirNodePath);

        playerInteractionDir.PlayerIsDead += PlayerDeadState;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected
    protected void MainMenuState() {
        // Main Menu with start/options/quit

        // 
    }

    protected void GameStartState() {
        // Load in the game and 
    }

    protected void PlayerDeadState() {
        // End player input and display stats

        // Offer the player a respawn or end game option
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}