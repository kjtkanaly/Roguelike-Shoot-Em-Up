using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerAttackDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerInteractionDirector interactionDir = null;
	private PlayerDataDirector dataDir = null;
	private Node attackTimersContainer = null;
	private List<Timer> attackTimerList = new List<Timer>();
	private int maxAttackCount = 6;
	private bool DemoMode = false;

	// Public
	public PlayerAttackObject[] attackList = null;
	
	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		GetUsefulNodes();

		// Grab the Attack Timer(s) Container and the available timers
		attackTimersContainer = GetNode<Node>("Weapon-Timer-Container");
		foreach (Node node in attackTimersContainer.GetChildren()) {
			attackTimerList.Add((Timer) node);
		}

		// Init the Attacks Array
		attackList = new PlayerAttackObject[maxAttackCount];

		if (DemoMode) {
			InitSomeAttacks();
		}
	}

	public override void _Process(double delta)
	{
		
	}

	//-------------------------------------------------------------------------
	// Methods
	private void GetUsefulNodes() {
		interactionDir = GetNode<PlayerInteractionDirector>("..");
		dataDir =  GetNode<PlayerDataDirector>("../../Player-Data-Director");
	}

	public int GetOpenActionSlotIndex() {
		int index = 0;

		for (index = 0; index < attackList.Length; index++) {
			if (attackList[index] == null) {
				return index;
			}
		}

		return -1;
	}

	public void InitAttackSlotObject(int index) {
		attackList[index] = new PlayerAttackObject();
	}

	public void SetAttackSlotIndex(int index) {
		attackList[index].SetAttackIndex(index);
	}

	public void SetAttackSlotData(int index, PlayerAttackData data) {
		attackList[index].SetData(data);
	}

	public void InitAttackSlotTimer(int index) {
		attackList[index].InitTimer(attackTimerList[index]);
	}	

	//-------------------------------------------------------------------------
	// Debug/Demo Methods
	public bool AddAttack(float damageVal, float delayVal, bool maxCountOverride=false) {
		// Null Catch
		if ((attackList.Length > maxAttackCount) && (!maxCountOverride)) {
			return false;
		}

		int newAttackIndex = GetOpenActionSlotIndex();

		// Update the Attack List
		attackList[newAttackIndex] = new PlayerAttackObject(
			damageVal, 
			delayVal,
			newAttackIndex,
			attackTimerList[newAttackIndex],
			AttackObject.Type.None);

		return true;
	}

	private void InitSomeAttacks() {
		AddAttack(10, 1);
		AddAttack(1,  5);	
	}

	private void PrintTimerNames() {
		foreach(Timer timer in attackTimerList) {
			GD.Print(timer.Name);
		}
	}
}
