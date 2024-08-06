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
    private double elapsedTime = 0.0f;

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
    public void UpdateTimer(double timeDelta) {
        // Log the elapsed time
        elapsedTime += timeDelta;
        
        // Convert the elappsed time to minutes and seconds
        double minutes = elapsedTime / 60;
        double seconds = elapsedTime % 60;

        // Update the Timer Label's text
        playTimeLabel.Text = 
            $"{minutes.ToString("#0")}:{seconds.ToString("00")}";
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
