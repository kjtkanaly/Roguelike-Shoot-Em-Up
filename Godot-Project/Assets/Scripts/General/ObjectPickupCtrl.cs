using Godot;
using System;
using System.Collections.Generic;

public partial class ObjectPickupCtrl : Area3D
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
    }

    //-------------------------------------------------------------------------
	// Methods
    public void AddFreeActionToNearbyList(Area3D freeActionArea) {
        nearbyFreeActionNodes.Add((Node3D) freeActionArea);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}