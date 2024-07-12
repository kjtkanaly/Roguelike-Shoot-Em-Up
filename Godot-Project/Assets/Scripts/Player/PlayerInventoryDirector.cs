using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerInventoryDirector : Node
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	// Protected
	protected List<AttackDirector> attackInventory = null;

	// Private
	private int maxAttackCount;
	private PlayerInteractionDirector interDir;

	//-------------------------------------------------------------------------
	// Methods
	public PlayerInventoryDirector(int maxAttackCountVal, PlayerInteractionDirector interDirInst) {
		attackInventory = new List<AttackDirector>();
		maxAttackCount = maxAttackCountVal;
		interDir = interDirInst;
	}

	public int IsActionAlreadyEquipped(string itemName) {
		for (int i = 0; i < attackInventory.Count; i++) {
			if (attackInventory[i].GetAttackId() == itemName) {
				return i;
			}
		}
		return -1;
	}

	private bool IsActionSlotMaxLevel(int index) { 
		if (attackInventory[index].level < attackInventory[index].GetAttackMaxLevel()) {
			return false;
		} else {
			return true;
		}
	}

	public bool IsPlayerMaxedOutOnAttacks() {
		if (attackInventory.Count >= maxAttackCount) {
			return true;
		} else {
			return false;
		}
	}

	public void EquipNewAttack(PackedScene newAttack) {
		AttackDirector newAttackObject = 
			(AttackDirector) newAttack.Instantiate().GetChild(0);
		interDir.AddChild(newAttackObject.GetParent());

		attackInventory.Add(newAttackObject);

		// Debug
		GD.Print($"Equiped New Attack: {newAttackObject.GetAttackData().id}");
		GD.Print($"Postion: {newAttackObject.Position}");
	}

	public bool LevelUpEquippedAction(int itemIndex) {
		// Check if the Action is below the max level
		if (IsActionSlotMaxLevel(itemIndex)) {
			return false;
		}

		// Increment the level
		attackInventory[itemIndex].LevelUpAttack();

		return true;
	}

	//-------------------------------------------------------------------------
	// Debug/Demo Methods
	private void InitSomeAttacks() {
	}
}
