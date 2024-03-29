using Godot;
using System;

public partial class PlayerAttackObject : AttackObject
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	// Public
	public Timer timer = null;
	public int attackIndex = -1;	
	//-------------------------------------------------------------------------
	// Game Events

	//-------------------------------------------------------------------------
	// Methods
	public PlayerAttackObject(float damageVal, float delayVal, int indexVal,Timer timerObj) : 
		base(damageVal, delayVal){
		attackIndex = indexVal;
		timer = timerObj;
		timer.Timeout += CallAttack;

		UpdateTimerTime();
		StartTimer();
	}

	public void SetAttackIndex(int index) {
		attackIndex = index;
	}

	private void UpdateTimerTime() {
		timer.WaitTime = delay;
	}

	private void StartTimer() {
		timer.Start();
	}

	private void CallAttack() {
		GD.Print($"Attack Index {attackIndex}: Time Delay = {delay}s");
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
