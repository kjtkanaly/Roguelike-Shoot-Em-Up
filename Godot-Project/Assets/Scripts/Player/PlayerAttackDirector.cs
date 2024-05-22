using Godot;
using System;

public partial class PlayerAttackDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	public Timer timer = null;
	public int level = 1;

	// Protected
	protected Node MainRoot;
	protected bool debug = false;
	protected MeshInstance3D meshInstance = null;

	// Private 
	[Export] private AttackData data = null;
	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		timer = GetAttackTimer();
		meshInstance = GetMeshInstance();
		MainRoot = GetTree().Root.GetChild(0);

		// Init the attack's timer
		InitTimer();

	}
	//-------------------------------------------------------------------------
	// Methods
	// Public Methods
	public virtual AttackData GetAttackData() {
		return data;
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

	public void InitTimer() {
		timer.Timeout += CallAttack;

		UpdateTimerTime();
		StartTimer();
	}

	public bool IsDataEmpty() {
		return data == null;
	}

	public virtual void SetVisuals() {
		
	}

	public virtual void SetColliderInformation() {
		
	}

	public virtual void LevelUpAttack() {
		GD.Print("Universal Level Up");
		level += 1;
	}

	public virtual string GetAttackId() {
		return data.id;
	}

	public virtual int GetAttackMaxLevel() {
		return data.maxLevel;
	}

	// Protected
	protected virtual void UpdateTimerTime() {
		timer.WaitTime = data.delay;
	}

	protected void StartTimer() {
		timer.Start();
	}

	protected virtual void CallAttack() {
		if (debug) {
			GD.Print($"Generic Attack:");
		}
	}

	// Private Methods

	//-------------------------------------------------------------------------
	// Debug Methods
}

