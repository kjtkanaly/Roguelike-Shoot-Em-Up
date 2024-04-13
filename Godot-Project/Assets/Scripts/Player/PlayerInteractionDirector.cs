using Godot;
using System;

public partial class PlayerInteractionDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerAttackDirector playerAttack = null;
	private PlayerDataDirector playerData = null;
	private PlayerObjectPickup pickupArea = null;

	// Public

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		playerAttack = GetNode<PlayerAttackDirector>("Player-Attack-Director");
		playerData = GetNode<PlayerDataDirector>("../Player-Data-Director");
	}

	//-------------------------------------------------------------------------
	// Methods
	public int GetOpenActionSlotIndex() {
		return playerAttack.GetOpenActionSlotIndex();
	}

	public void SetAttackSlotObjectProps(int index, PlayerAttackData data) {
		playerAttack.SetAttackSlotObjectProps(index, data);
	}

	public bool IsActionAlreadyEquipped(string id) {
		return playerAttack.IsActionAlreadyEquipped(id);
	}

	public bool LevelUpEquippedAction(PlayerAttackData attackData) {
		return playerAttack.LevelUpEquippedAction(attackData);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}