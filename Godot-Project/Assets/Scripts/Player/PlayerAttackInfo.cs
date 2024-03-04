using Godot;
using System;

public partial class PlayerAttackInfo
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	// Public
	public int attackIndex = -1;
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

	public void SetAttackIndex(int index) {
		attackIndex = index;
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
