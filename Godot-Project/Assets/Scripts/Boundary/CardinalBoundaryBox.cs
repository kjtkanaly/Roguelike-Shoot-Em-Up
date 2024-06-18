using Godot;
using System;

public partial class CardinalBoundaryBox : Area3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string adjacentAreaPath = "";

    // Protected

    // Private
    private Area3D adjacentArea;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
	{
		base._Ready();

        GetAdjacentArea();

        BodyEntered += TeleportPlayer;
	}

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private
    private void GetAdjacentArea() {
        adjacentArea = GetNode<Area3D>(adjacentAreaPath);
    }

    private void TeleportPlayer(Node3D playerNode) {
        GD.Print($"Object Entered: {playerNode.Name}");

        playerNode.Position = new Vector3(0, 1, 0);
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}