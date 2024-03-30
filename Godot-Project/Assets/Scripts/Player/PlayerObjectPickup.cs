using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerObjectPickup : Area3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerInteractionDirector interactionDir = null; 

	// Public
	public List<Node3D> nearbyFreeActionNodes = null;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		interactionDir = GetNode<PlayerInteractionDirector>("..");

		nearbyFreeActionNodes = new List<Node3D>();

		AreaEntered += AddFreeActionToNearbyList;
		AreaExited += RemoveFreeActionFromNearbyList;
	}

	//-------------------------------------------------------------------------
	// Methods
	private void AddFreeActionToNearbyList(Area3D freeActionArea) {
		GD.Print($"{freeActionArea.Name} is within range");
		nearbyFreeActionNodes.Add((Node3D) freeActionArea);

		// To DO: Move the following behind input logic later on
		// See sequence diagram note for more context
		PickupFirstFreeAction();
	}

	private void RemoveFreeActionFromNearbyList(Area3D freeActionArea) {
		GD.Print($"{freeActionArea.Name} is out of range");
		nearbyFreeActionNodes.Remove((Node3D) freeActionArea);
	}

	private bool PickupFirstFreeAction() {
		// Get the player's next open action slot index
		int index = interactionDir.GetOpenActionSlotIndex();
		if (index == -1) {
			GD.Print("No free attack slots");
			return false;
		}
		GD.Print($"Attack Slot {index} is open");

		// Get the First Free Action's Attack data
		FreeAction freeAction = (FreeAction) nearbyFreeActionNodes[0];

		// Init the open action slot's Attack Object
		interactionDir.SetAttackSlotObjectProps(index, freeAction.attackData);

		// Destroy the now equipped action node
		nearbyFreeActionNodes[0].QueueFree();

		return true;
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
