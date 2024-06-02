using Godot;
using System;

public partial class PlayerInteractionDirector : InteractionDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public int maxAttackCount = 6;

	// Protected

	// Private
	private PlayerInventoryDirector inventoryDir = null;
	private PlayerObjectPickup pickupArea = null;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		pickupArea = GetNode<PlayerObjectPickup>("../Item-Pickup-Director");
		inventoryDir = new PlayerInventoryDirector(maxAttackCount);
	}

	//-------------------------------------------------------------------------
	// Methods
	public int IsActionAlreadyEquipped(string itemName) {
		return inventoryDir.IsActionAlreadyEquipped(itemName);
	}

	public bool IsPlayerMaxedOutOnAttacks() {
		return inventoryDir.IsPlayerMaxedOutOnAttacks();
	}

	public bool LevelUpEquippedAction(int itemIndex) {
		return inventoryDir.LevelUpEquippedAction(itemIndex);
	}

	public void EquipNewAttack(PackedScene newAttack) {
		inventoryDir.EquipNewAttack(newAttack);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
