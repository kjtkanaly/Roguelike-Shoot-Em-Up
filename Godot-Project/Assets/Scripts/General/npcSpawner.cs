using Godot;
using System;

public partial class NpcSpawner : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	[Export] private PackedScene npcNode;
	private Node mainRoot;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		GetMainRoot();
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public void SpawnNpc() {
		Node3D npcNodeInst = (Node3D) npcNode.Instantiate();
		npcNodeInst.Position = this.Position;

		mainRoot.AddChild(npcNodeInst);
	}

	// Protected

	// Private
	private void GetMainRoot() {
		mainRoot = GetTree().Root.GetChild(0);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
