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
    private GameStats gameStats;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        foreach (Node node in GetTree().GetNodesInGroup("Game Stats")) {
            gameStats = (GameStats) node;
        }
        gameStats.UpdateKillCount += IncrementKillCountLabel;
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
        playerStatusBox.InitHealthUI(maxHealth);
    }

    public string GetCurrentTime_MMSS() {
        return timerBox.GetCurrentTime_MMSS();
    }

    public int GetCandyCount() {
        return playerStatusBox.GetCandyCount();
    }

    public void IncrementKillCountLabel() {
        playerStatusBox.IncrementKillCountLabel(gameStats.killCount);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
