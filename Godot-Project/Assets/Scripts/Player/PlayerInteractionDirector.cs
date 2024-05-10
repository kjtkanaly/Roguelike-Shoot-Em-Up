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
	public int GetOpenActionSlotIndex() {
		return playerAttackSlotsDir.GetOpenActionSlotIndex();
	}

	public void InitAttackSlotObject(int index, AttackData data) {
		playerAttackSlotsDir.InitAttackSlotObject(index, data);
	}

	public bool IsActionAlreadyEquipped(string id) {
		return playerAttackSlotsDir.IsActionAlreadyEquipped(id);
	}

	public bool LevelUpEquippedAction(AttackData attackData) {
		return playerAttackSlotsDir.LevelUpEquippedAction(attackData);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
