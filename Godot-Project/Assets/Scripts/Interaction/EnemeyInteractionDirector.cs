using Godot;
using System;

public partial class EnemeyInteractionDirector : NPCInteractionDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	private EnemyInteractionData interactionData;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();
	}

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override EnemyInteractionData GetInteractionData() {
		return interactionData;
	}

	// Protected
	protected override void LoadInteractionData() {
		interactionData = (EnemyInteractionData) GD.Load(interactionDataPath);
	}

	protected override void BeginDeathSequence() {
		base.BeginDeathSequence();
	}

	// Private

	//-------------------------------------------------------------------------
	// Debug Methods

}
