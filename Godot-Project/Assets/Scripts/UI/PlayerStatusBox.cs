using Godot;
using System;

public partial class PlayerStatusBox : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string healthBarNodePath;
    [Export] public string healthLabelNodePath;
    [Export] public string candyCountLabelNodePath;

    // Protected

    // Private
    private TextureProgressBar healthBar;
    private Label healthLabel;
    private Label candyCountLabel;
    private int candyCount = 0;
    private int enemiesSlain = 0;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready() {
        InitObjectRefs();    
    }

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void UpdateHealthUI(AttackData data) {
        // Decrement the remaining health by the damage
        healthBar.Value -= data.damage;
        healthLabel.Text = healthBar.Value.ToString();
    }

    public void InitHealthUI(float maxHealth) {
        InitObjectRefs();

        healthBar.MaxValue = maxHealth;
        healthBar.Value = maxHealth;
        healthLabel.Text = maxHealth.ToString();
    }

    public int GetCandyCount() {
        return candyCount;
    }

    public int GetEnemiesSlain() {
        return enemiesSlain;
    }

    // Protected

    // Private
    private void InitObjectRefs() {
        healthBar = GetNode<TextureProgressBar>(healthBarNodePath);
        healthLabel = GetNode<Label>(healthLabelNodePath);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
