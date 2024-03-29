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
    public void AddFreeActionToNearbyList(Area3D freeActionArea) {
        GD.Print($"{freeActionArea.Name} is within range");
        nearbyFreeActionNodes.Add((Node3D) freeActionArea);

        // To DO: Move the following behind input logic later on
        // See sequence diagram note for more context
        PickupFirstFreeAction();
    }

    public void RemoveFreeActionFromNearbyList(Area3D freeActionArea) {
        GD.Print($"{freeActionArea.Name} is out of range");
        nearbyFreeActionNodes.Remove((Node3D) freeActionArea);
    }

    public void RemoveFreeActionFromNearbyList(Node3D freeActionNode) {
        GD.Print($"{freeActionNode.Name} was picked up");
        nearbyFreeActionNodes.Remove(freeActionNode);
    }

    private bool PickupFirstFreeAction() {
        // Get the player's next open action slot index
        int index = interactionDir.GetOpenActionSlotIndex();
        if (index == -1) {
            return false;
        }

        return true;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}