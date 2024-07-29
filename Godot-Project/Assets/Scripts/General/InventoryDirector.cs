using Godot;
using System;
using System.Collections.Generic;
using GDCollect = Godot.Collections;

public partial class InventoryDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	// Protected
	protected List<AttackDirector> attackInventory = null;

	// Private
	[Export] private int maxAttackCount = 6;
	private GDCollect.Array<StringName> groupNames;

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public override void _Ready() {
		base._Ready();

		groupNames = GetGroups();
		attackInventory = new List<AttackDirector>();

		EquipPreloadedAttack();
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

		AddAttackToInventory(newAttackObject);
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

	public void DisableAttacks() {
		for (int i = 0; i < attackInventory.Count; i++) {
			attackInventory[i].DisableAttack();
		}
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

	private void EquipPreloadedAttack() {
		foreach (Node3D child in GetChildren()) {
			if (!child.GetChild(0).IsInGroup("Attack Object")) {
				continue;
			}
			AddAttackToInventory((AttackDirector) child.GetChild(0));
		}
	}

	private void AddAttackToInventory(AttackDirector attack) {
		foreach (string groupName in groupNames) {
			attack.AddToGroup(groupName);
		}
		attackInventory.Add(attack);
	}
}
