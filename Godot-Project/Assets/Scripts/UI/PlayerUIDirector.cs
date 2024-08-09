using Godot;
using System;

public partial class PlayerUIDirector : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string playerUINodePath;
    [Export] public string pauseGameUINodePath;
    public bool gamePaused = false;

    // Protected

    // Private
    private PlayerInGameUI playerUI;
    private PauseGameUI pauseGameUI;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        InitObjectRefs();

        pauseGameUI.ResumeGame += TogglePause;
    }

    public override void _Process(double delta) {
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
        InitObjectRefs();
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

        GD.Print($"Game Paused: {gamePaused}");
    }

    // Protected

    // Private
    private void InitObjectRefs() {
        playerUI = GetNode<PlayerInGameUI>(playerUINodePath);
        pauseGameUI = GetNode<PauseGameUI>(pauseGameUINodePath);
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
