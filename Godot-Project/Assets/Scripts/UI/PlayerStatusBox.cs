using Godot;
using System;

public partial class PlayerStatusBox : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private TextureProgressBar healthBar;
    [Export] private Label healthLabel;
    [Export] private Label candyCountLabel;
    [Export] private Label killCountLabel;
    private PlayerStats playerStats;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
    }

    public override void _Process(double delta)
    {
        // Update the Candy Count Label
        candyCountLabel.Text = playerStats.candyCount.ToString();

        // Update the Kill Count Label
        killCountLabel.Text = playerStats.killCount.ToString();

        // Update the Player's Heath Bar
        healthBar.Value = playerStats.health;
        healthBar.MaxValue = playerStats.maxHealth;
        healthLabel.Text = playerStats.health.ToString();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private
    

    //-------------------------------------------------------------------------
    // Debug Methods
}
