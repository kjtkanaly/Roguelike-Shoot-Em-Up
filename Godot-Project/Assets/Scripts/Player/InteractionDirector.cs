using Godot;
using System;

public partial class InteractionDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private PlayerAttackDirector playerAttack = null;
	private PlayerDataDirector playerData = null;
	private ObjectPickupCtrl pickupArea = null;

	// Public

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		playerAttack = GetNode<PlayerAttackDirector>("Player-Attack-Director");
		playerData = GetNode<PlayerDataDirector>("../Player-Data-Director");
	}

	//-------------------------------------------------------------------------
	// Methods


	//-------------------------------------------------------------------------
	// Debug Methods
}
