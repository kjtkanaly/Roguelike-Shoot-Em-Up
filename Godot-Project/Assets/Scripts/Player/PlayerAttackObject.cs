using Godot;
using System;

public partial class PlayerAttackObject : AttackObject
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private bool debug = false;
	private MeshInstance3D meshInstance = null;
	// Public
	public Timer timer = null;
	public int attackIndex = -1;
	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		timer = GetAttackTimer();
		meshInstance = GetMeshInstance();
		SetMainRootVar();
	}
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

	public Timer GetAttackTimer() {
		foreach (Node node in GetChildren()) {
			if (node.Name == "Attack Timer") {
				return (Timer) node;
			}
		}

		return null;
	}

	public MeshInstance3D GetMeshInstance() {
		foreach (Node node in GetChildren()) {
			if (node.Name == "Attack Mesh") {
				return (MeshInstance3D) node;
			}
		}

		return null;
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

	public void SetVisuals(PlayerAttackData data) {
		if (data.type == AttackObject.Type.AreaOfEffect) {
			meshInstance.Mesh = data.areaMesh;
		}
	}

	private void UpdateTimerTime() {
		timer.WaitTime = data.delay;
	}

	private void StartTimer() {
		timer.Start();
	}

	private void CallAttack() {
		if (debug) {
			GD.Print($"Attack Index {attackIndex}: Time Delay = {data.delay}s");
		}

		if (data.type == AttackObject.Type.Projectile) {
			ProjectileAttackSequence();
		} else if (data.type == AttackObject.Type.AreaOfEffect) {

		} else if (data.type == AttackObject.Type.Melee) {

		} else {

		}
	}

	public void LevelUpAttack() {
		level += 1;

		// Type Specefic Level Up Actions
		switch(data.type) {
			case Type.Projectile:
				break;
			case Type.AreaOfEffect:
				Scale += new Vector3(1, 1, 1) * data.lvlUpAreaStepSize;
				break;
			case Type.Melee:
				break;            
		}
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
