using Godot;
using System;

public partial class GameOverUI : UI
{
    public struct GameOverStats {
        public string candyCount;
        public string enemiesSlain;
        public string totalTime;

        public GameOverStats(string candyCountVal, string enemiesSlainVal, string totalTimeVal){
            candyCount = candyCountVal;
            enemiesSlain = enemiesSlainVal;
            totalTime = totalTimeVal;
        }
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string titleMenuScenePath;
    [Export] public string newGameButtonNodePath;
    [Export] public string quitButtonNodePath;
    [Export] public string candyLabelNodePath;
    [Export] public string totalTimeLabelNodePath;
    [Export] public string enemiesSlainLabelNodePath;

    // Protected

    // Private
    private Button newGameButton;
    private Button quitButton;
    private Label candyLabel;
    private Label totalTimeLabel;
    private Label enemiesSlainLabel;
    private GameOverStats stats;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        newGameButton = GetNode<Button>(newGameButtonNodePath);
        quitButton = GetNode<Button>(quitButtonNodePath);
        candyLabel = GetNode<Label>(candyLabelNodePath);
        totalTimeLabel = GetNode<Label>(totalTimeLabelNodePath);
        enemiesSlainLabel = GetNode<Label>(enemiesSlainLabelNodePath);
        

        newGameButton.ButtonUp += BeginNewGame;
        quitButton.ButtonUp += LoadTitleMenu;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void UpdateStatsObject(GameOverStats newStats) {
        totalTimeLabel.Text += newStats.totalTime;
        candyLabel.Text += newStats.candyCount;
        enemiesSlainLabel.Text += newStats.enemiesSlain;
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
