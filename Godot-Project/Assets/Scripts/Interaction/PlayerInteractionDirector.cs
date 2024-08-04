using Godot;
using System;
using Godot.Collections;

public partial class PlayerInteractionDirector : InteractionDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal]
    public delegate void PlayerIsDeadEventHandler();

    // Protected

    // Private
    private PlayerInteractionData interactionData;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        itemPickupDir.NewAttackNearby += PickupFirstFreeAttack;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override PlayerInteractionData GetInteractionData() {
        return interactionData;
    }

    // Protected
    protected override void LoadInteractionData() {
        interactionData = (PlayerInteractionData) GD.Load(interactionDataPath);
    }

    protected override void PickupFirstFreeAttack() {
        // Get the first nearby item's pickup dir ref
        AttackPickupDir freeAttack = itemPickupDir.nearbyFreeActionNodes[0];

        // Get the nearby item's name
        string attackName = freeAttack.GetAttackName();

        // Check if we have already picked this up (Level Up or New Attack)
        int attackIndex = inventoryDir.GetAttackIndex(attackName);

        GD.Print($"\n{attackName} Index: {attackIndex}");

        // Attack is already equipped and needs to be leveled up
        if (attackIndex != -1) {
            // Level Up Action
            inventoryDir.LevelUpEquippedAction(attackIndex);
        } else {
            // Equip New Action
            PackedScene attackPackedScene = freeAttack.GetAttackPackedScene();
            inventoryDir.EquipNewAttack(attackPackedScene);
        }

        // Destroy the now equipped action node
        freeAttack.QueueFree();
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
