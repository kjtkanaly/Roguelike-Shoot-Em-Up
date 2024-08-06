using Godot;
using System;

public partial class PlayerStatusBox : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string healthBarNodePath;
    [Export] public string healthLabelNodePath;

    // Protected

    // Private
    private TextureProgressBar healthBar;
    private Label healthLabel;

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

    // Protected

    // Private
    private void InitObjectRefs() {
        healthBar = GetNode<TextureProgressBar>(healthBarNodePath);
        healthLabel = GetNode<Label>(healthLabelNodePath);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
