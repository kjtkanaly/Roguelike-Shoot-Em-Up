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
	public PlayerAttackObject(PlayerAttackData dataVal, int indexVal, Timer timerObj) : 
		base(dataVal){
		attackIndex = indexVal;
		timer = timerObj;
		timer.Timeout += CallAttack;

		UpdateTimerTime();
		StartTimer();
	}

	public PlayerAttackObject() : base(){
		attackIndex = -1;
		timer = null;
		data = null;
	}

	public void SetAttackIndex(int index) {
		attackIndex = index;
	}

	public void SetData(PlayerAttackData dataVal) {
		data = dataVal;
	}

	public void InitTimer(Timer timerInstance) {
		timer = timerInstance;
		timer.Timeout += CallAttack;

		UpdateTimerTime();
		StartTimer();
	}

	private void UpdateTimerTime() {
		timer.WaitTime = data.delay;
	}

	private void StartTimer() {
		timer.Start();
	}

	private void CallAttack() {
		GD.Print($"Attack Index {attackIndex}: Time Delay = {data.delay}s");

		if (data.type == AttackObject.Type.Projectile) {
			ProjectileAttackSequence();
		} else if (data.type == AttackObject.Type.AreaOfEffect) {

		} else if (data.type == AttackObject.Type.Melee) {

		} else {

		}
	}

	private void CallProjectileAttack() {
		
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
