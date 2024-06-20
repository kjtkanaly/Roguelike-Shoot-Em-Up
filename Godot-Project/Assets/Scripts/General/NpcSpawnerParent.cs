using Godot;
using System;
using System.Collections.Generic;

public partial class NpcSpawnerParent : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	private List<NpcSpawner> spawnerList = new List<NpcSpawner>();
	private Timer spawnTimer;
	[Export] private float spawnTimerWaitTime = 1.0f;
	[Export] private bool spawnTimerOneShot = true;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		// Get all the spawners in the scene
		foreach (Node node in GetChildren()) {
			if (node.IsInGroup("NPC-Spawner")) {
				spawnerList.Add((NpcSpawner) node);
			}
		}

		// Get the Spawner Timer
		InitSpawnTimer();
		
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public

	// Protected

	// Private
	private void InitSpawnTimer() {
		spawnTimer = GetNode<Timer>("Spawn-Timer");
		spawnTimer.Timeout += SpawnEnemies;
		spawnTimer.WaitTime = spawnTimerWaitTime;
		spawnTimer.OneShot = spawnTimerOneShot;
		spawnTimer.Start();
	}

	private void SpawnEnemies() {
		GD.Print("Spawning Enemies");

		foreach (NpcSpawner spawner in spawnerList) {
			spawner.SpawnNpc();
		}
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
