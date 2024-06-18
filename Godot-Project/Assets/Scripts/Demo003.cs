using Godot;
using System;
using System.Collections.Generic;

public partial class Demo003 : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	private List<npcSpawner> spawnerList = new List<npcSpawner>();

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		Node3D npcSpawnerParent = GetNode<Node3D>("NPC-Spawner-Parent");
		Godot.Collections.Array<Godot.Node> spawnerNodes = npcSpawnerParent.GetChildren();

		foreach (Node node in npcSpawnerParent.GetChildren()) {
			spawnerList.Add((npcSpawner) node);
		}
		
		foreach (npcSpawner spawner in spawnerList) {
			spawner.SpawnNpc();
		}
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public

	// Protected

	// Private

	//-------------------------------------------------------------------------
	// Debug Methods
}
