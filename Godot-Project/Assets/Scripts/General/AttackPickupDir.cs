using Godot;
using System;

public partial class AttackPickupDir : Area3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public PackedScene attackObject;
	
	// Protected

	// Private
	[Export] private AttackData data;

	//-------------------------------------------------------------------------
	// Game Events

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public virtual AttackData GetData() {
		return data;
	}

	// Protected

	// Private

	//-------------------------------------------------------------------------
	// Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TO DO: Do some shit
