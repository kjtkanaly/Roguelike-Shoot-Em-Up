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
	private Node3D playerNode;

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

		// Get the player node
		playerNode = GetNode<Node3D>("../Player-Director");

		// Initialize the Spawner Timer
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
		NpcSpawner spawner = GetFurthestSpawnerFromPlayer();

		// Defautl Check
		if (spawner == null) {
			GD.PushError("Null furthest spawner");
		}

		spawner.SpawnNpc();
	}

	private NpcSpawner GetFurthestSpawnerFromPlayer() {
		Vector2 playerPos = new Vector2(playerNode.Position.X, 
										playerNode.Position.Z);
		float maxDistance = 0.0f;
		NpcSpawner furthestSpawner = null;

		foreach (NpcSpawner spawner in spawnerList) {
			float dist2Player = spawner.GetDistanceToPoint(playerPos);
			if (dist2Player > maxDistance) {
				maxDistance = dist2Player;
				furthestSpawner = spawner;
			}
		}

		return furthestSpawner;
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
