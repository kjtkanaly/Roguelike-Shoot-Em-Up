using Godot;
using System;

public partial class EnemyMovementDirector : NPCMovementDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private EnemyMovementData movementData;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        base._Ready();
    }

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public override void LoadMovementkData() {
		movementData = (EnemyMovementData) GD.Load(movementDataPath);
	}

    public override float GetMass() {
		return movementData.mass;
	}

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}