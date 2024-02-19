using Godot;
using System;
using System.Collections.Generic;

public class AttackObject 
{
	public PlayerAttackInfo attackInfo = null;
	public Timer timer = null;

	public AttackObject(PlayerAttackInfo attackInfoObj, Timer timerObj) {
		attackInfo = attackInfoObj;
		timer = timerObj;

		timer.Timeout += CallAttack;

		UpdateTimerTime();
		StartTimer();
	}

	private void UpdateTimerTime() {
		timer.WaitTime = attackInfo.delay;
	}

	private void StartTimer() {
		timer.Start();
	}

	private void CallAttack() {
		GD.Print($"Attack Delay: {attackInfo.delay}s");
	}
}

public partial class PlayerAttackDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private Node attackTimersContainer = null;
	private List<Timer> attackTimerList = new List<Timer>();
	private int maxAttackCount = 6;
	private bool DemoMode = true;

	// Public
	public AttackObject[] attackList = null;
	
	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		// Grab the Attack Timer(s) Container and the available timers
		attackTimersContainer = GetNode<Node>("Weapon-Timer-Container");
		foreach (Node node in attackTimersContainer.GetChildren()) {
			attackTimerList.Add((Timer) node);
		}

		// Init the Attacks Array
		attackList = new AttackObject[maxAttackCount];

		if (DemoMode) {
			InitSomeAttacks();
		}
	}

	public override void _Process(double delta)
	{
		
	}

	//-------------------------------------------------------------------------
	// Methods
	public bool AddAttack(PlayerAttackInfo playerAttack, bool maxCountOverride=false) {
		// Null Catch
		if ((attackList.Length > maxAttackCount) && (!maxCountOverride)) {
			return false;
		}

		int newAttackIndex = GetNextAttackIndex();

		// Update the Attack List
		attackList[newAttackIndex] = new AttackObject(
			playerAttack, 
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
		AddAttack(new PlayerAttackInfo(10, 1));
		AddAttack(new PlayerAttackInfo(1,  5));	
	}

	private void PrintTimerNames() {
		foreach(Timer timer in attackTimerList) {
			GD.Print(timer.Name);
		}
	}
}
