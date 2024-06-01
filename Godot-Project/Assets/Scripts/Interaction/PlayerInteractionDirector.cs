using Godot;
using System;

public partial class PlayerInteractionDirector : InteractionDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerAttackSlotsDirector attackDir = null;
	private PlayerDataDirector playerData = null;
	private PlayerObjectPickup pickupArea = null;

	// Public

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		attackDir = GetNode<PlayerAttackSlotsDirector>("Player-Attack-Director");
		playerData = GetNode<PlayerDataDirector>("../Player-Data-Director");
	}

	//-------------------------------------------------------------------------
	// Methods
	public int IsActionAlreadyEquipped(string itemName) {
		return attackDir.IsActionAlreadyEquipped(itemName);
	}

	public bool IsPlayerMaxedOutOnAttacks() {
		return attackDir.IsPlayerMaxedOutOnAttacks();
	}

	public bool LevelUpEquippedAction(int itemIndex) {
		return attackDir.LevelUpEquippedAction(itemIndex);
	}

	public void EquipNewAttack(PackedScene newAttack) {
		attackDir.EquipNewAttack(newAttack);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
