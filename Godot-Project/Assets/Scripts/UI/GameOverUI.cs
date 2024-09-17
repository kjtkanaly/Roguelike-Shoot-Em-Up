using Godot;
using System;

public partial class GameOverUI : UI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string titleMenuScenePath;

    // Protected
    protected PlayerStats playerStats;

    // Private
    [Export] private Button newGameButton;
    [Export] private Button quitButton;
    [Export] private Label candyLabel;
    [Export] private Label totalTimeLabel;
    [Export] private Label enemiesSlainLabel;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        newGameButton.ButtonUp += BeginNewGame;
        quitButton.ButtonUp += LoadTitleMenu;

        // Get the global player stats object
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void UpdateStatsObject() {
        totalTimeLabel.Text += playerStats.GetCurrentTime_MMSS();
        candyLabel.Text += playerStats.candyCount.ToString();
        enemiesSlainLabel.Text += playerStats.killCount.ToString();
    }

    public void RefreshUI() {
        
    }

    // Protected

    // Private
    private void LoadTitleMenu() {
        GetTree().ChangeSceneToFile(titleMenuScenePath);
    }

    private void BeginNewGame() {
        GetTree().ReloadCurrentScene();
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
