using Godot;
using System;

public partial class GameStats : Node
{
    //-------------------------------------------------------------------------
    // Signals
    [Signal]
    public delegate void UpdateKillCountEventHandler();

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float gameTime;
    public int killCount;

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void IncrementKillCount() {
        killCount += 1;
        EmitSignal(SignalName.UpdateKillCount);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
