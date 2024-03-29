using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerObjectPickup : Area3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Private
    // Public
    public List<Node3D> nearbyFreeActionNodes = null;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready() {
        nearbyFreeActionNodes = new List<Node3D>();

        AreaEntered += AddFreeActionToNearbyList;
        AreaExited += RemoveFreeActionFromNearbyList;
    }

    //-------------------------------------------------------------------------
	// Methods
    public void AddFreeActionToNearbyList(Area3D freeActionArea) {
        GD.Print($"{freeActionArea.Name} is within range");
        nearbyFreeActionNodes.Add((Node3D) freeActionArea);
    }

    public void RemoveFreeActionFromNearbyList(Area3D freeActionArea) {
        GD.Print($"{freeActionArea.Name} is out of range");
        nearbyFreeActionNodes.Remove((Node3D) freeActionArea);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}