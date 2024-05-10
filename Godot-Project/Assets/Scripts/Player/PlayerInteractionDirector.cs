using Godot;
using System;

public partial class PlayerInteractionDirector : Node
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerAttackSlotsDirector playerAttack = null;
	private PlayerDataDirector playerData = null;
	private PlayerObjectPickup pickupArea = null;

	// Public

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		playerAttack = GetNode<PlayerAttackSlotsDirector>("Player-Attack-Director");
		playerData = GetNode<PlayerDataDirector>("../Player-Data-Director");
	}

	//-------------------------------------------------------------------------
	// Methods
	public int GetOpenActionSlotIndex() {
		return playerAttack.GetOpenActionSlotIndex();
	}

	public void SetAttackSlotObjectProps(int index, AttackData data) {
		playerAttack.SetAttackSlotObjectProps(index, data);
	}

	public bool IsActionAlreadyEquipped(string id) {
		return playerAttack.IsActionAlreadyEquipped(id);
	}

	public bool LevelUpEquippedAction(AttackData attackData) {
		return playerAttack.LevelUpEquippedAction(attackData);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
