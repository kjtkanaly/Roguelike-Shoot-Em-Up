using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerObjectPickup : Area3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerInteractionDirector interactionDir = null; 
	private bool debug = true;

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
		if (debug) {
			GD.Print($"{freeActionArea.Name} is within range");
		}

		nearbyFreeActionNodes.Add((Node3D) freeActionArea);

		// To DO: Move the following behind input logic later on
		// See sequence diagram note for more context
		PickupFirstFreeAction();
	}

	private void RemoveFreeActionFromNearbyList(Area3D freeActionArea) {
		if (debug) {
			GD.Print($"{freeActionArea.Name} is out of range");
		}
		nearbyFreeActionNodes.Remove((Node3D) freeActionArea);
	}

	private bool PickupFirstFreeAction() {
		// Get the First Free Action's Attack data
		FreeAction freeAction = (FreeAction) nearbyFreeActionNodes[0];

		// Check if we have already picked this up (Level Up or New Attack)
		bool alreadyEquipped = interactionDir.IsActionAlreadyEquipped(
			freeAction.attackData.id);

		if (alreadyEquipped) {
			// Level Up Action
			GD.Print($"Already Pickuped {freeAction.attackData.id}");
			LevelUpEquippedAction(freeAction);
		} else {
			// Equip New Action
			GD.Print($"New Attack");
			return EquipNewFreeAction(freeAction);
		}

		return true;
	}

	private bool EquipNewFreeAction(FreeAction freeAction) {
		// Get the player's next open action slot index
		int index = interactionDir.GetOpenActionSlotIndex();
		if (index == -1) {
			if (debug) {
				GD.Print("No free attack slots");
			}
			
			return false;
		}

		if (debug) {
			GD.Print($"Attack Slot {index} is open");
		}

		// Init the open action slot's Attack Object
		interactionDir.SetAttackSlotObjectProps(index, freeAction.attackData);

		// Destroy the now equipped action node
		nearbyFreeActionNodes[0].QueueFree();

		return true;
	}

	private bool LevelUpEquippedAction(FreeAction freeAction) {
		return true;
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
