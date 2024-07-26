using Godot;
using System;
using System.Collections.Generic;
using GDCollect = Godot.Collections;

public partial class RepetativeAttackDirector : AttackDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    protected Dictionary<ulong, SceneTreeTimer> enemyTimers = 
		new Dictionary<ulong, SceneTreeTimer>(); 

    // Private
    private RepetativeAttackData data = null;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        base._Ready();

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
		data = (RepetativeAttackData) GD.Load(dataPath);
	}

    public override RepetativeAttackData GetAttackData() {
        return data;
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

		// Check if the entered area is this or an ally
		GDCollect.Array<Godot.StringName> otherGroups = 
			otherIteraction.GetGroups();
		foreach (string otherGroup in otherGroups) {
			GD.Print($"Other Area's Group: {otherGroup}");
			if (IsInGroup(otherGroup)) {
				return;
			}
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

    //-------------------------------------------------------------------------
	// Debug Methods
}