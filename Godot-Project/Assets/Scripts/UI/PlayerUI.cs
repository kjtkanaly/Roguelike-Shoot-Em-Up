using Godot;
using System;

public partial class PlayerUI : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string healthBarPath;
    [Export] public string interactionDirNodePath;

    // Protected

    // Private
    private TextureProgressBar healthBar;
    private InteractionDirector interactionDir;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        healthBar = GetNode<TextureProgressBar>(healthBarPath);
        interactionDir = GetNode<InteractionDirector>(interactionDirNodePath);
        interactionDir.TookDamage += UpdateHealthUI;

        InitMaxHelth();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void UpdateHealthUI(AttackData data) {
        healthBar.Value -= (double) data.damage;
    }

    public void InitMaxHelth() {
        healthBar.MaxValue = interactionDir.GetInteractionData().maxHealth;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
