using Godot;
using System;

public partial class PauseGameUI : UI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal]
    public delegate void ResumeGameEventHandler();
    [Export] public string titleMenuFilePath;

    // Protected

    // Private
    [Export] private Button resumeButton;
    [Export] private Button quitButton;
    // [Export] private GameDataDirector gameDataDir;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready() {
        resumeButton.ButtonUp += ResumeButtonClicked;
        quitButton.ButtonUp += SaveAndQuitButtonClicked;

        // gameDataDir = new GameDataDirector();
    }

    //-------------------------------------------------------------------------
	// Methods
    // Public

    // Protected

    // Private
    private void ResumeButtonClicked() {
        EmitSignal(SignalName.ResumeGame);
    }

    private void SaveAndQuitButtonClicked() {
        // gameDataDir.SaveGameState(GetTree().GetNodesInGroup("Persist"));
        EmitSignal(SignalName.ResumeGame);
        GetTree().ChangeSceneToFile(titleMenuFilePath);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
