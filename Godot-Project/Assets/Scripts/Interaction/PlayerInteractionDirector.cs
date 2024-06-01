using Godot;
using System;

public partial class PlayerInteractionDirector : Node
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerAttackSlotsDirector playerAttackSlotsDir = null;
	private PlayerDataDirector playerData = null;
	private PlayerObjectPickup pickupArea = null;

	// Public

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		playerAttackSlotsDir = GetNode<PlayerAttackSlotsDirector>("Player-Attack-Director");
		playerData = GetNode<PlayerDataDirector>("../Player-Data-Director");
	}

	//-------------------------------------------------------------------------
	// Methods
	public int IsActionAlreadyEquipped(string itemName) {
		return playerAttackSlotsDir.IsActionAlreadyEquipped(itemName);
	}

	public bool IsPlayerMaxedOutOnAttacks() {
		return playerAttackSlotsDir.IsPlayerMaxedOutOnAttacks();
	}

	public bool LevelUpEquippedAction(int itemIndex) {
		return playerAttackSlotsDir.LevelUpEquippedAction(itemIndex);
	}

	public void EquipNewAttack(PackedScene newAttack) {
		playerAttackSlotsDir.EquipNewAttack(newAttack);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
