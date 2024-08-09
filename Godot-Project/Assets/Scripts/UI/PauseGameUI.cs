using Godot;
using System;

public partial class PauseGameUI : UI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal]
    public delegate void ResumeGameEventHandler();
    [Export] public string resumeButtonNodePath;
    [Export] public string saveAndQuitButtonNodePath;
    [Export] public string titleMenuFilePath;

    // Protected

    // Private
    private Button resumeButton;
    private Button saveAndQuitButton;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready() {
        resumeButton = GetNode<Button>(resumeButtonNodePath);
        saveAndQuitButton = GetNode<Button>(saveAndQuitButtonNodePath);

        resumeButton.ButtonUp += ResumeButtonClicked;
        saveAndQuitButton.ButtonUp += SaveAndQuitButtonClicked;
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
        EmitSignal(SignalName.ResumeGame);
        GetTree().ChangeSceneToFile(titleMenuFilePath);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
