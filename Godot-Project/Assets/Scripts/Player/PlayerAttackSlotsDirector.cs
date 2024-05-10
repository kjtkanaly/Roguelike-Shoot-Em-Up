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

	public int GetOpenActionSlotIndex() {
		int index = 0;

		for (index = 0; index < attackList.Count; index++) {
			if (attackList[index].IsDataEmpty()) {
				return index;
			}
		}

		return -1;
	}

	public void SetAttackSlotObjectProps(int index, AttackData data) {
		// Update the open action slot's index value
		SetAttackSlotIndex(index);

		// Update the open action slot with the Free Action's Attack data
		SetAttackSlotData(index, data);

		// Activate the open action slot's timer
		InitAttackSlotTimer(index);

		// TO DO: Consider just making a general init attack object fx
		// Update any visuals for the attack
		SetAttackVisuals(index);

		// Set Collider Information
		SetColliderInformation(index);
	}

	private void SetAttackSlotIndex(int index) {
		attackList[index].SetAttackIndex(index);
	}

	private void SetAttackSlotData(int index, AttackData data) {
		attackList[index].SetData(data);
	}

	private void InitAttackSlotTimer(int index) {
		attackList[index].InitTimer(attackTimerList[index]);
	}	

	private void SetAttackVisuals(int index) {
		attackList[index].SetVisuals();
	}

	private void SetColliderInformation(int index) {
		attackList[index].SetColliderInformation();
	}

	public bool IsActionAlreadyEquipped(string id) {
		for (int i = 0; i < attackList.Count; i++) {
			if (attackList[i].IsDataEmpty()) {
				continue;
			}

			if (attackList[i].GetAttackId() == id) {
				return true;
			}
		}

		return false;
	}

	private int GetEquippedActionSlotIndex(string id) {
		for (int i = 0; i < attackList.Count; i++) {
			if (attackList[i].IsDataEmpty()) {
				continue;
			}

			if (attackList[i].GetAttackId() == id) {
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

	public bool LevelUpEquippedAction(AttackData data) {
		// Get the Action index
		int index = GetEquippedActionSlotIndex(data.id);

		// Check if the Action is below the max level
		if (IsActionSlotMaxLevel(index)) {
			return false;
		}

		// Increment the level
		attackList[index].LevelUpAttack();

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
