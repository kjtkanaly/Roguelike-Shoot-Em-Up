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
	public PlayerAttackObject(float damageVal, float delayVal, int indexVal, Timer timerObj, AttackObject.Type typeVal) : 
		base(damageVal, delayVal, typeVal){
		attackIndex = indexVal;
		timer = timerObj;
		timer.Timeout += CallAttack;

		UpdateTimerTime();
		StartTimer();
	}

	public PlayerAttackObject() :
		base(){
		attackIndex = -1;
		timer = null;
	}

	public void SetAttackIndex(int index) {
		attackIndex = index;
	}

	public void SetData(PlayerAttackData data) {
		type = data.type;
		damage = data.damage;
		delay = data.delay;
	}

	public void InitTimer(Timer timerInstance) {
		timer = timerInstance;
		timer.Timeout += CallAttack;

		UpdateTimerTime();
		StartTimer();
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
