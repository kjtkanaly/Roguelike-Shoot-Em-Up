using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class AoEAttackDirector : RepetativeAttackDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	
	// Protected

	// Private
	private AoEData data = null;
	Dictionary<ulong, SceneTreeTimer> enemyTimers = 
		new Dictionary<ulong, SceneTreeTimer>(); 
	// GetTree().CreateTimer(1.0f)

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		SetAoEObjects();
		SetCollisionMaskValues();    

		hitBoxDirector.AreaEntered += AddEnemyTimer;
		hitBoxDirector.AreaExited += RemoveEnemyTimer;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		// Go through the dictionary and see which targets can take damage
		CheckEnemyTimers();
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public override void LoadAttackDataFile() {
		// Start here 7/10/2024
		data = (AoEData) GD.Load(dataPath);
	}

	public override AoEData GetAttackData() {
		return data;
	}

	public override void SetVisuals() {
		attackMesh.Mesh = data.areaMesh;
	}

	public override void SetColliderInformation() {
		hitBoxShape.Shape = data.areaColliderShape;
	}

	public override void LevelUpAttack() {
		base.LevelUpAttack();

		Scale += new Vector3(1, 1, 1) 
				 * ((AoEData) data).areaIncreaseStepSize;
	}

	public override string GetAttackId() {
		return data.id;
	}

	public override int GetAttackMaxLevel() {
		return data.maxLevel;
	}

	// Protected
	protected void AddEnemyTimer(Area3D enemyArea) {
		// Get the Opposing objects Interaction Director
		InteractionDirector otherIteraction = 
			enemyArea.GetNode<InteractionDirector>("..");

		// Safety Check
		if (otherIteraction == null) {
			GD.Print($"{enemyArea.Name} had no Interaction Area");
			return;
		}
		
		// Take the Initial Damage
		otherIteraction.TickHealth(GetAttackData().damage);

		// Create the timer
		SceneTreeTimer timer = GetTree().CreateTimer(GetAttackData().delay);
		
		// Log the Opposing Area and it's timer
		enemyTimers.Add(otherIteraction.GetInstanceId(), timer);

		if (debug) {
			GD.Print($"{otherIteraction.GetInstanceId()} has entered the AoE");
		}
	}

	protected void RemoveEnemyTimer(Area3D enemyArea) {
		// Get the Opposing objects Interaction Director
		InteractionDirector otherIteraction = 
			enemyArea.GetNode<InteractionDirector>("..");

		// Safety Check
		if (otherIteraction == null) {
			GD.Print($"{enemyArea.Name} had no Interaction Area");
			return;
		}

		// Get the enemy's interaction dir Unique ID
		ulong enemyID = otherIteraction.GetInstanceId();

		// Remove the enemy's damage timer from the dictionary
		enemyTimers.Remove(enemyID);

		if (debug) {
			GD.Print($"{enemyID} has left the AoE");
		}
	}

	protected void CheckEnemyTimers() {
		// Loop through the current list of enemy timers
		foreach(KeyValuePair<ulong, SceneTreeTimer> enemyTimer in enemyTimers) {
			if (enemyTimer.Value.TimeLeft <= 0) {
				InteractionDirector enemyInteraction = 
					(InteractionDirector) InstanceFromId(enemyTimer.Key);

				if (enemyInteraction == null) {
					enemyTimers.Remove(enemyTimer.Key);
				}
				
				enemyInteraction.TickHealth(GetAttackData().damage);
				
				// Reset the EnemyTimer
				enemyTimers[enemyTimer.Key] = GetTree().CreateTimer(GetAttackData().delay);
			}
		}
	}

	// Private
	private void SetAoEObjects() {
		hitBoxDirector = GetNode<Area3D>("Hit-Box-Director");
		hitBoxShape = hitBoxDirector.GetNode<CollisionShape3D>("Hit-Box-Shape");
	}
}
