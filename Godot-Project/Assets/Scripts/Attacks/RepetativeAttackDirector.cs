using Godot;
using System;

public partial class RepetativeAttackDirector : AttackDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private RepetativeAttackData data = null;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        base._Ready();
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

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}