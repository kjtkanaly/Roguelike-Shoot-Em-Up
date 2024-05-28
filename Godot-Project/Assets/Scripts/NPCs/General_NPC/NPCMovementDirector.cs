using Godot;
using System;

public partial class NPCMovementDirector : MovementDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
	private NPCMovementData movementData;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
	}

    //-------------------------------------------------------------------------
	// Methods
    // Public
	public override void LoadMovementkData() {
		movementData = (NPCMovementData) GD.Load(movementDataPath);
	}

	public override float GetMass() {
		return movementData.mass;
	}

    // Protected

    // Private

	//-------------------------------------------------------------------------
	// Debug Methods
}