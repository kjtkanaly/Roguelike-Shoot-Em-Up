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
    public int candyCount;
    public int killCount;

    // Protected

    // Private
    private double elapsedTime = 0.0f;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Process(double delta)
    {
        // Log the elapsed time
        elapsedTime += delta;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    public void IncrementKillCount() {
        killCount += 1;
        EmitSignal(SignalName.UpdateKillCount);
    }

    public string GetCurrentTime_MMSS() {
        // Convert the elappsed time to minutes and seconds
        double minutes = elapsedTime / 60;
        double seconds = elapsedTime % 60;

        return $"{minutes.ToString("#0")}:{seconds.ToString("00")}";
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
