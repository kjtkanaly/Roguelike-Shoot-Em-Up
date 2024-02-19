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
        
		UpdateTimerTime(attackInfo.delay);
		StartTimer();
	}

	private void UpdateTimerTime(float delay) {
		timer.WaitTime = attackInfo.delay;
	}

	private void StartTimer() {
		timer.Start();
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

		// Init the Attack and Timer Arrays
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
		if ((attackList.Length >= maxAttackCount) && !maxCountOverride) {
			return false;
		}

		int newAttackIndex = attackList.Length - 1;

		// Update the Attack List
		attackList[newAttackIndex] = new AttackObject(
			playerAttack, 
			attackTimerList[newAttackIndex]);

		return true;
	}

    private void CallAttack() {

    }
	//-------------------------------------------------------------------------
	// Debug/Demo Methods
	private void InitSomeAttacks() {
		AddAttack(new PlayerAttackInfo(10, 1));
		AddAttack(new PlayerAttackInfo(1,  5));
		
	}
}
