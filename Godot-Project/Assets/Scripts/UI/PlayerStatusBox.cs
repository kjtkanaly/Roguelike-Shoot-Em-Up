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
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init(PlayerUIDirector playerUIRef) {
    }

    public void UpdateHealthUI(AttackData data) {
        // Decrement the remaining health by the damage
        healthBar.Value -= data.damage;
        healthLabel.Text = healthBar.Value.ToString();
    }

    public void InitHealthUI(float maxHealth) {
        healthBar.MaxValue = maxHealth;
        healthBar.Value = maxHealth;
        healthLabel.Text = maxHealth.ToString();
    }

    public void IncrementKillCountLabel(int count) {
        killCountLabel.Text = count.ToString();
    }

    // Protected

    // Private
    

    //-------------------------------------------------------------------------
    // Debug Methods
}
