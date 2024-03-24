using Godot;
using System;
using System.Collections.Generic;

public class AttackObject 
{
    public enum Type {
        None = 0,
        Projectile = 1,
        AreaOfEffect = 2
    }

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
		GD.Print($"Attack Index {attackInfo.attackIndex}: Time Delay = {attackInfo.delay}s");
	}
}