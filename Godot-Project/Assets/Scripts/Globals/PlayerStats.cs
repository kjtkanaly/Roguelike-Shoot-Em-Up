using Godot;
using System;

public partial class PlayerStats : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float gameTime = 0.0f;
    public float health = 0.0f;
    public float maxHealth = 0.0f;
    public int candyCount = 0;
    public int killCount = 0;

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
    public string GetCurrentTime_MMSS() {
        // Convert the elappsed time to minutes and seconds
        double minutes = elapsedTime / 60;
        double seconds = elapsedTime % 60;

        return $"{minutes.ToString("#0")}:{seconds.ToString("00")}";
    }

    public void IncrementKillCount() {
        killCount += 1;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
