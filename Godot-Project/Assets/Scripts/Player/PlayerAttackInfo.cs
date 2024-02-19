using Godot;
using System;

public partial class PlayerAttackInfo
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	// Public
	[Export] public float damage = 1.0f;
	[Export] public float delay = 1.0f;
	//-------------------------------------------------------------------------
	// Game Events

	//-------------------------------------------------------------------------
	// Methods
	public PlayerAttackInfo(float damageVal, float delayVal) {
		damage = damageVal;
		delay = delayVal;
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
