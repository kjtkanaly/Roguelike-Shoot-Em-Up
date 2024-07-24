using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	// Protected
	protected List<AttackDirector> attackInventory = null;

	// Private
	[Export] private int maxAttackCount = 6;

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public override void _Ready() {
		base._Ready();

		attackInventory = new List<AttackDirector>();
	}

	public int GetAttackIndex(string itemName) {
		for (int i = 0; i < attackInventory.Count; i++) {
			GD.Print($"{attackInventory[i].attackName} == {itemName}");
			if (attackInventory[i].attackName == itemName) {
				return i;
			}
		}
		return -1;
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
		this.AddChild(newAttackObject.GetParent());

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

	// Protected
	
	// Private
	private bool IsActionSlotMaxLevel(int index) { 
		if (attackInventory[index].level < attackInventory[index].GetAttackMaxLevel()) {
			return false;
		} else {
			return true;
		}
	}
}
