using Godot;
using System;

public partial class InteractionDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected
	protected Area3D HitBoxDir;
	protected CollisionShape3D  HitBoxShape;
	protected Timer TakeDamageTimer;

	// Private

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		HitBoxDir = GetNode<Area3D>("Hit-Box-Director");
		HitBoxShape = GetNode<CollisionShape3D>("Hit-Box-Director/Hit-Box-Shape");
		TakeDamageTimer = GetNode<Timer>("Take-Damage-Timer");
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public

	// Protected

	// Private

	//-------------------------------------------------------------------------
	// Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TO DO: Do some shit
