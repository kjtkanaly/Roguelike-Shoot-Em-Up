using Godot;
using System;

public partial class NPCInteractionDirector : InteractionDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	private NPCInteractionData interactionData;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		base._Ready();
	}


	//-------------------------------------------------------------------------
	// Methods
	// Public
	public override NPCInteractionData GetInteractionData() {
		return interactionData;
	}

	// Protected
	protected override void LoadInteractionData() {
		interactionData = (NPCInteractionData) GD.Load(interactionDataPath);
	}

	// Private


	//-------------------------------------------------------------------------
	// Debug Methods
}
