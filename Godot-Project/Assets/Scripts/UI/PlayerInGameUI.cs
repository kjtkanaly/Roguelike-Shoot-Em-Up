using Godot;
using System;

public partial class PlayerInGameUI : UI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string playerStatusBoxNodePath;
    [Export] public string timerBoxNodePath;

    // Protected

    // Private
    private PlayerStatusBox playerStatusBox;
    private TimerBox timerBox;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        InitObjectRefs();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        timerBox.UpdateTimer(delta);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void UpdateHealthUI(AttackData data) {
        playerStatusBox.UpdateHealthUI(data);
    }

    public void InitHealthUI(float maxHealth) {
        InitObjectRefs();

        playerStatusBox.InitHealthUI(maxHealth);
    }

    // Protected

    // Private
    private void InitObjectRefs() {
        playerStatusBox = GetNode<PlayerStatusBox>(playerStatusBoxNodePath);
        timerBox =GetNode<TimerBox>(timerBoxNodePath);
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
