using Godot;
using System;

public partial class PlayerDataDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	[Export] public PlayerMovementData movementData; // = (PlayerMovementData) GD.Load("res://Assets/Resources/Movement Data Files/default_player_001.tres");
	private PlayerInventoryDirector InventoryDir;

	// Godot Types

	// Basic Types

	//-------------------------------------------------------------------------
	// Game Events

	//-------------------------------------------------------------------------
	// Methods
	public int GetInventoryItem(int index) {
		return InventoryDir.GetInventoryItem(index);
	}

	//-------------------------------------------------------------------------
	// Demo Methods

}
