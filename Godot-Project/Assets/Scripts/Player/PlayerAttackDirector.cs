using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerAttackDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private InteractionDirector interactionDir = null;
	private PlayerDataDirector dataDir = null;
	private Node attackTimersContainer = null;
	private List<Timer> attackTimerList = new List<Timer>();
	private int maxAttackCount = 6;
	private bool DemoMode = true;

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
		interactionDir = GetNode<InteractionDirector>("..");
		dataDir =  GetNode<PlayerDataDirector>("../../Player-Data-Director");
	}

	public bool AddAttack(float damageVal, float delayVal, bool maxCountOverride=false) {
		// Null Catch
		if ((attackList.Length > maxAttackCount) && (!maxCountOverride)) {
			return false;
		}

		int newAttackIndex = GetNextAttackIndex();

		// Update the Attack List
		attackList[newAttackIndex] = new PlayerAttackObject(
			damageVal, 
			delayVal,
			newAttackIndex,
			attackTimerList[newAttackIndex]);

		return true;
	}

	private int GetNextAttackIndex() {
		int index = 0;

		for (index = 0; index < attackList.Length; index++) {
			if (attackList[index] == null) {
				return index;
			}
		}

		return -1;
	}

	//-------------------------------------------------------------------------
	// Debug/Demo Methods
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
