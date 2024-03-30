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
	private int maxAttackCount = 0;
	private bool DemoMode = false;

	// Public
	public PlayerAttackObject[] attackList = null;
	
	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		interactionDir = GetNode<PlayerInteractionDirector>("..");

		// Get all of the potential Attack Timers
		GetAttackTimers();

		// Init the Attack Object List 
		InitAttackObjectList();

		if (DemoMode) {
			InitSomeAttacks();
		}
	}

	public override void _Process(double delta)
	{
		
	}

	//-------------------------------------------------------------------------
	// Methods
	private void InitAttackObjectList() {
		// Set the maxAttackCount equal the number available timers
		maxAttackCount = attackTimerList.Count;

		// Init the Attacks Array to be equal in length to the number of timers
		attackList = new PlayerAttackObject[maxAttackCount];

		// Loop through the array and contruct the attack objects
		for (int i = 0; i < maxAttackCount; i++) {
			attackList[i] = new PlayerAttackObject();
			AddChild(attackList[i]);
			attackList[i].SetMainRootVar();
		}
	}

	private void GetAttackTimers() {
		// Grab the Attack Timer(s) Container
		attackTimersContainer = GetNode<Node>("Weapon-Timer-Container");

		// Iterate thru the child timers
		foreach (Node node in attackTimersContainer.GetChildren()) {
			attackTimerList.Add((Timer) node);
		}
	}

	public int GetOpenActionSlotIndex() {
		int index = 0;

		for (index = 0; index < attackList.Length; index++) {
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

	public bool IsActionAlreadyEquipped(string id) {
		for (int i = 0; i < attackList.Length; i++) {
			if (attackList[i].data == null) {
				continue;
			}

			if (attackList[i].data.id == id) {
				return true;
			}
		}

		return false;
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
