using Godot;
using System;

public partial class TimerBox : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    protected PlayerStats playerStats;

    // Private
    [Export] private Label playTimeLabel;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        // Get the global player stats object
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        playTimeLabel.Text = playerStats.GetCurrentTime_MMSS();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void UpdateTimerLabel(string newTime) {
        // Update the Timer Label's text
        playTimeLabel.Text = newTime;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
