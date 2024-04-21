using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerAttackDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerInteractionDirector interactionDir = null;
	private Node attackTimersContainer = null;
	private List<Timer> attackTimerList = new List<Timer>();
	private int attackCount = 0;
	private bool DemoMode = false;

	// Public
	public List<PlayerAttackObject> attackList = null;
	
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
		attackList = new List<PlayerAttackObject>();

		foreach (PlayerAttackObject attackObject in GetChildren()) {
			attackList.Add(attackObject);
		}

		attackCount = attackList.Count;
	}

	private void GetAttackTimers() {
		// Iterate thru the attack objects
		foreach (PlayerAttackObject attackObject in attackList) {
			attackTimerList.Add(attackObject.GetAttackTimer());
		}
	}

	public int GetOpenActionSlotIndex() {
		int index = 0;

		for (index = 0; index < attackList.Count; index++) {
			if (attackList[index].data == null) {
				return index;
			}
		}

		return -1;
	}

	public void SetAttackSlotObjectProps(int index, PlayerAttackData data) {
		// Update the open action slot's index value
		SetAttackSlotIndex(index);

		// Update the open action slot with the Free Action's Attack data
		SetAttackSlotData(index, data);

		// Activate the open action slot's timer
		InitAttackSlotTimer(index);

		// Update any visuals for the attack
		SetAttackVisuals(index, data);

		// Set Collider Information
		SetColliderInformation(index, data);
	}

	private void SetAttackSlotIndex(int index) {
		attackList[index].SetAttackIndex(index);
	}

	private void SetAttackSlotData(int index, PlayerAttackData data) {
		attackList[index].SetData(data);
	}

	private void InitAttackSlotTimer(int index) {
		attackList[index].InitTimer(attackTimerList[index]);
	}	

	private void SetAttackVisuals(int index, PlayerAttackData data) {
		attackList[index].SetVisuals(data);
	}

	private void SetColliderInformation(int index, PlayerAttackData data) {
		attackList[index].SetColliderInformation(data);
	}

	public bool IsActionAlreadyEquipped(string id) {
		for (int i = 0; i < attackList.Count; i++) {
			if (attackList[i].data == null) {
				continue;
			}

			if (attackList[i].data.id == id) {
				return true;
			}
		}

		return false;
	}

	private int GetEquippedActionSlotIndex(string id) {
		for (int i = 0; i < attackList.Count; i++) {
			if (attackList[i].data == null) {
				continue;
			}

			if (attackList[i].data.id == id) {
				return i;
			}
		}

		return -1;
	}

	private bool IsActionSlotMaxLevel(int index) { 
		if (attackList[index].level < attackList[index].data.maxLevel) {
			return false;
		} else {
			return true;
		}
	}

	public bool LevelUpEquippedAction(PlayerAttackData data) {
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
