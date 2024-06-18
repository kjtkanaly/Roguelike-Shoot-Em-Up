using Godot;
using System;

public partial class npcSpawner : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private PackedScene npcNode;
    private Node MainRoot;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        MainRoot = MainRoot = GetTree().Root.GetChild(0);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void SpawnNpc( ) {
        Node3D damageLabelInst = (Node3D) npcNode.Instantiate();
        damageLabelInst.Position = this.Position;
		MainRoot.AddChild(damageLabelInst);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}