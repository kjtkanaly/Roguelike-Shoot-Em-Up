using Godot;
using System;

public partial class NPCInteractionDirector : InteractionDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	private float currentAoEDamage = 0.0f;
	private NPCInteractionData interactionData;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready() {
		base._Ready();
	}


	//-------------------------------------------------------------------------
	// Methods
	// Public

	// Protected
	protected override void LoadInteractionData() {
		interactionData = (NPCInteractionData) GD.Load(interactionDataPath);
	}

	// Private

	private void StopSequence(Area3D otherArea) {
		if (otherArea.Name == "Hit-Box-Director") {
			StopAoEDamageSequence();
		}
	}

	private void StopAoEDamageSequence() {
		// Reset currentAoEDamage
		currentAoEDamage = 0.0f;

		// Stop Damage TImer
		takeDamageTimer.Stop();
	}

	private void SettakeDamageTimerProps(float waitTimeVal, bool oneShotVal) {
		takeDamageTimer.WaitTime = waitTimeVal;
		takeDamageTimer.OneShot = oneShotVal;
	}

	private void ProjectileDamageSequence(Node3D projNode) {
		// Get the projectile's damage
		float damage = ((ProjectileDir) projNode).damage;

		// Take damage from the projectile
		TickHealth(damage);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
