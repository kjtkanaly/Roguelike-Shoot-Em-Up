using Godot;
using System;

public partial class PlayerUIDirector : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public bool gamePaused = false;
    public bool gameOver = false;

    // Protected

    // Private
    [Export] private PlayerInGameUI playerUI;
    [Export] private PauseGameUI pauseGameUI;
    [Export] private GameOverUI gameOverUI;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        pauseGameUI.ResumeGame += TogglePause;
    }

    public override void _Process(double delta) {

        // Player Input
        if (gameOver) {
            return;
        }

        if (Input.IsActionJustPressed("Pause Game")) {
            TogglePause();
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void UpdateHealthUI(AttackData data) {
        playerUI.UpdateHealthUI(data);
    }

    public void InitHealthUI(float maxHealth) {
        playerUI.InitHealthUI(maxHealth);
    }

    public void TogglePause() {
        // Swap the UI's
        pauseGameUI.ToggleVisible(!pauseGameUI.Visible);
        playerUI.ToggleVisible(!playerUI.Visible);

        if (!gamePaused) {
            GetTree().Paused = true;    // Pause the game engine
            gamePaused = true;          // Log that the game is paused
        } else {
            GetTree().Paused = false;   // Unpause the game engine
            gamePaused = false;         // Log that the game is resumed
        }
    }

    public void GoToGameOver() {
        // Log Game Over
        gameOver = true;

        // Create the Game Over Stats Struct
        gameOverUI.UpdateStatsObject(
            new GameOverUI.GameOverStats(
                playerUI.GetCandyCount().ToString(), 
                "Scooped out", 
                playerUI.GetCurrentTime_MMSS()));

        gameOverUI.RefreshUI();

        // Swap the UI's
        pauseGameUI.ToggleVisible(false);
        playerUI.ToggleVisible(false);
        gameOverUI.ToggleVisible(true);
        
    }   

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
