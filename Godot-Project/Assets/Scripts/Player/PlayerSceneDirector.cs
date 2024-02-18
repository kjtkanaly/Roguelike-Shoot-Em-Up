using Godot;
using System;
using System.Collections.Generic;

// This class should handle things such as
// - Pause Game
// - Quit Game
// - Resume Game
// - Scene Control
public partial class PlayerSceneDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	private Timer QuitGameTimer = null;

	// Godot Types

	// Basic Types
	private const float quitGameDelay = 2.0f;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		QuitGameTimer = GetNode<Timer>("Quit-Game");

		Input.MouseMode = Input.MouseModeEnum.Captured;

		QuitGameTimer.Timeout += QuitGame;
	}

	public override void _Process(double delta)
	{
		ToggleQuitGame();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventAction) {
			if (eventAction.IsActionPressed("Pause Game")) {
				TogglePause();
			}
		}
	}

	//-------------------------------------------------------------------------
	// Player Scene Director Methods
	public void TogglePause() {
		if (!GetTree().Paused) {
			PauseGame();
		} else {
			ResumeGame();
		}
	}

	private void PauseGame() {
		Input.MouseMode = Input.MouseModeEnum.Visible;
		GetTree().Paused = true;

		GD.Print("Pause Game");
	}

	private void ResumeGame() {
		Input.MouseMode = Input.MouseModeEnum.Captured;
		GetTree().Paused = false;
	}

	public void ToggleQuitGame() {
		if (Input.IsActionJustPressed("Quit Game"))
			QuitGameTimer.Start(quitGameDelay);
		if (Input.IsActionJustReleased("Quit Game"))
			QuitGameTimer.Stop();
	}

	private void QuitGame() {
		GetTree().Quit();
	}

	//-------------------------------------------------------------------------
	// Demo Methods
}
