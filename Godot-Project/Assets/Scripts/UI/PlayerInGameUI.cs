using Godot;
using System;

public partial class PlayerInGameUI : UI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private PlayerStatusBox playerStatusBox;
    [Export] private TimerBox timerBox;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void UpdateHealthUI(AttackData data) {
        playerStatusBox.UpdateHealthUI(data);
    }

    public void InitHealthUI(float maxHealth) {
        playerStatusBox.InitHealthUI(maxHealth);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
