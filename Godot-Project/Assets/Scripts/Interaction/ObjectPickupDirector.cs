using Godot;
using System;
using System.Collections.Generic;

public partial class ObjectPickupDirector : Area3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal]
    public delegate void NewAttackNearbyEventHandler();
    public List<AttackPickupDir> nearbyFreeActionNodes = null;

    // Private
    private bool debug = false;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready() {
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

        EmitSignal(SignalName.NewAttackNearby);
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
