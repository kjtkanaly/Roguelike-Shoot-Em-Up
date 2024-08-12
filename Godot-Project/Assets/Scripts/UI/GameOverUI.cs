using Godot;
using System;

public partial class GameOverUI : UI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string titleMenuScenePath;
    [Export] public string newGameButtonNodePath;
    [Export] public string quitButtonNodePath;

    // Protected

    // Private
    private Button newGameButton;
    private Button quitButton;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        newGameButton = GetNode<Button>(newGameButtonNodePath);
        quitButton = GetNode<Button>(quitButtonNodePath);

        newGameButton.ButtonUp += BeginNewGame;
        quitButton.ButtonUp += LoadTitleMenu;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

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
