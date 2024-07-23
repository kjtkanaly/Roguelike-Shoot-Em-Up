using Godot;
using System;
using System.Collections.Generic;

public partial class ObjectPickupDirector : Area3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Private
    private PlayerInteractionDirector interactionDir = null; 
    private bool debug = false;

    // Public
    public List<AttackPickupDir> nearbyFreeActionNodes = null;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready() {
        interactionDir = GetNode<PlayerInteractionDirector>("../Generic-Interaction-Director");

        nearbyFreeActionNodes = new List<AttackPickupDir>();

        AreaEntered += AddFreeActionToNearbyList;
        AreaExited += RemoveFreeActionFromNearbyList;
    }

    //-------------------------------------------------------------------------
    // Methods
    private void AddFreeActionToNearbyList(Area3D freeAttackPickupArea) {
        if (!freeAttackPickupArea.IsInGroup("Attack-Pickup")) {
            GD.Print($"Player Pickup Controller encountered an area that isn't an attack pickup");
            return;
        }

        nearbyFreeActionNodes.Add((AttackPickupDir) freeAttackPickupArea);
    }

    private void RemoveFreeActionFromNearbyList(Area3D freeAttackPickupArea) {
        if (!freeAttackPickupArea.IsInGroup("Attack-Pickup")) {
            GD.Print($"Player Pickup Controller encountered an area that isn't an attack pickup");
            return;
        }
        
        nearbyFreeActionNodes.Remove((AttackPickupDir) freeAttackPickupArea);
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
