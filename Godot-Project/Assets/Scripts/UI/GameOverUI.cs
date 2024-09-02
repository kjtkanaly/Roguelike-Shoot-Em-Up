using Godot;
using System;

public partial class GameOverUI : UI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string titleMenuScenePath;

    // Protected

    // Private
    [Export] private Button newGameButton;
    [Export] private Button quitButton;
    [Export] private Label candyLabel;
    [Export] private Label totalTimeLabel;
    [Export] private Label enemiesSlainLabel;
    private GameStats gameStats;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        newGameButton.ButtonUp += BeginNewGame;
        quitButton.ButtonUp += LoadTitleMenu;

        // Get the Game Stats object
        foreach (Node node in GetTree().GetNodesInGroup("Game Stats")) {
            gameStats = (GameStats) node;
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void UpdateStatsObject() {
        totalTimeLabel.Text += gameStats.gameTime.ToString();
        candyLabel.Text += gameStats.candyCount.ToString();
        enemiesSlainLabel.Text += gameStats.killCount.ToString();
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
