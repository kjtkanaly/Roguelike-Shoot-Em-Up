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
        if (!gamePaused) {
            gamePaused = true;
            pauseGameUI.ToggleVisible(true);
            playerUI.ToggleVisible(false);
            Engine.TimeScale = 0;
        } else {
            gamePaused = false;
            pauseGameUI.ToggleVisible(false);
            playerUI.ToggleVisible(true);
            Engine.TimeScale = 1;
        }
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
