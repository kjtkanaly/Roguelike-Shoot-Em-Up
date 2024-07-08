using Godot;
using System;

public partial class MeleeAttackDirector : SingularAttackDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private MeleeAttackData data = null;

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
		data = (MeleeAttackData) GD.Load(dataPath);
	}
    
    public override MeleeAttackData GetAttackData() {
		return data;
	}

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}