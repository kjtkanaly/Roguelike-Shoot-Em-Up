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
    private PlayerUIDirector playerUI;
    private int candyCount = 0;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void Init(PlayerUIDirector playerUIRef) {
        playerUI = playerUIRef;
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

    public int GetCandyCount() {
        return candyCount;
    }

    public void IncrementKillCountLabel(int count) {
        killCountLabel.Text = count.ToString();
    }

    // Protected

    // Private
    

    //-------------------------------------------------------------------------
	// Debug Methods
}
