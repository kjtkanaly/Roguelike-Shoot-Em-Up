using Godot;
using System;

public partial class SingularAttackDirector : AttackDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private SingularAttackData data = null;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public override void LoadAttackDataFile() {
		data = (SingularAttackData) GD.Load(dataPath);
	}

    public override SingularAttackData GetAttackData() {
        return data;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}