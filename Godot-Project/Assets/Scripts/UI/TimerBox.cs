using Godot;
using System;

public partial class TimerBox : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string playTimeNodePath;

    // Protected

    // Private
    private Label playTimeLabel;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        playTimeLabel = GetNode<Label>(playTimeNodePath);
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
