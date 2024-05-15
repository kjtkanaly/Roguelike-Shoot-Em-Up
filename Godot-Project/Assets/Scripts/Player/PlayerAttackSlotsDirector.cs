using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerAttackSlotsDirector : Node
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	public List<PlayerAttackDirector> attackList = null;

	// Private
	private PlayerInteractionDirector interactionDir = null;
	private Node attackTimersContainer = null;
	private List<Timer> attackTimerList = new List<Timer>();
	private int attackCount = 0;
	private int maxAttackCount = 6;
	private bool DemoMode = false;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		interactionDir = GetNode<PlayerInteractionDirector>("..");

		// Get all of the Player Attack Objects
		GetPlayerAttackObjects();

		// Get all of the potential Attack Timers
		GetAttackTimers();

		if (DemoMode) {
			InitSomeAttacks();
		}
	}

	public override void _Process(double delta)
	{
		
	}

	//-------------------------------------------------------------------------
	// Methods
	private void GetPlayerAttackObjects() {
		attackList = new List<PlayerAttackDirector>();

		foreach (PlayerAttackDirector attackObject in GetChildren()) {
			attackList.Add(attackObject);
		}

		attackCount = attackList.Count;
	}

	private void GetAttackTimers() {
		// Iterate thru the attack objects
		foreach (PlayerAttackDirector attackObject in attackList) {
			attackTimerList.Add(attackObject.GetAttackTimer());
		}
	}

	public int IsActionAlreadyEquipped(string itemName) {
		for (int i = 0; i < attackList.Count; i++) {
			if (attackList[i].GetAttackId() == itemName) {
				return i;
			}
		}
		return -1;
	}

	private bool IsActionSlotMaxLevel(int index) { 
		if (attackList[index].level < attackList[index].GetAttackMaxLevel()) {
			return false;
		} else {
			return true;
		}
	}

	public bool IsPlayerMaxedOutOnAttacks() {
		if (attackList.Count >= maxAttackCount) {
			return true;
		} else {
			return false;
		}
	}

	public void EquipNewAttack(PackedScene newAttack) {
		PlayerAttackDirector newAttackObject = 
			(PlayerAttackDirector) newAttack.Instantiate().GetChild(0);
		this.AddChild(newAttackObject.GetParent());

		attackList.Add(newAttackObject);

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
		attackList[itemIndex].LevelUpAttack();

		return true;
	}

	//-------------------------------------------------------------------------
	// Debug/Demo Methods
	private void InitSomeAttacks() {
	}

	private void PrintTimerNames() {
		foreach(Timer timer in attackTimerList) {
			GD.Print(timer.Name);
		}
	}
}
