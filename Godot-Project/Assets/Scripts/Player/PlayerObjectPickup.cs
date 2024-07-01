using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerObjectPickup : Area3D
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
    private void AddFreeActionToNearbyList(Area3D freeActionArea) {
        if (debug) {
            GD.Print($"{freeActionArea.Name} is within range");
        }

        nearbyFreeActionNodes.Add((AttackPickupDir) freeActionArea);

        // TO DO: Move the following behind input logic later on
        // See sequence diagram note for more context
        PickupFirstFreeAction();
    }

    private void RemoveFreeActionFromNearbyList(Area3D freeActionArea) {
        if (debug) {
            GD.Print($"{freeActionArea.Name} is out of range");
        }
        nearbyFreeActionNodes.Remove((AttackPickupDir) freeActionArea);
    }

    private bool PickupFirstFreeAction() {
        // Get the nearby item's name
        string itemName = (string) nearbyFreeActionNodes[0].GetMeta("Item_Name");

        // Check if we have already picked this up (Level Up or New Attack)
        int attackIndex = interactionDir.IsActionAlreadyEquipped(itemName);

        if (attackIndex != -1) {
            // Level Up Action
            if (!interactionDir.LevelUpEquippedAction(attackIndex)) {
                return false;
            }
        } else {
            // Equip New Action
            if (!EquipNewFreeAction(nearbyFreeActionNodes[0].attackObject)) {
                return false;
            }
        }

        // Destroy the now equipped action node
        nearbyFreeActionNodes[0].QueueFree();

        // To Do: Update player UI
        return true;
    }

    private bool EquipNewFreeAction(PackedScene newAttack) {
        // Check if the player can equip more attacks
        if (interactionDir.IsPlayerMaxedOutOnAttacks()) {
            return false;
        }
        // Init the open action slot's Attack Object
        interactionDir.EquipNewAttack(newAttack);

        return true;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
