using Godot;
using System;

public partial class TitleMenu : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string playButtonNodePath;
    [Export] public string optionsButtonNodePath;
    [Export] public string quitButtonNodePath;
    [Export] public string WorldOneScenePath;

    // Protected

    // Private
    private Button playButton;
    private Button optionsButton;
    private Button quitButton;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        playButton = GetNode<Button>(playButtonNodePath);
        optionsButton = GetNode<Button>(optionsButtonNodePath);
        quitButton = GetNode<Button>(quitButtonNodePath);

        playButton.ButtonUp += PlayReleased;
        optionsButton.ButtonUp += OptionsReleased;
        quitButton.ButtonUp += QuitReleased;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private
    private void PlayReleased() {
        GetTree().ChangeSceneToFile(WorldOneScenePath);
    }

    private void OptionsReleased() { 
    }

    private void QuitReleased() {
        GetTree().Quit();
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
