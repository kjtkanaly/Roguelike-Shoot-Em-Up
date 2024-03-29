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

	public void InitAttackSlotObject(int index) {
		playerAttack.InitAttackSlotObject(index);
	}

	public void SetAttackSlotIndex(int index) {
		playerAttack.SetAttackSlotIndex(index);
	}

	public void SetAttackSlotData(int index, PlayerAttackData data) {
		playerAttack.SetAttackSlotData(index, data);
	}

	public void InitAttackSlotTimer(int index) {
		playerAttack.InitAttackSlotTimer(index);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
