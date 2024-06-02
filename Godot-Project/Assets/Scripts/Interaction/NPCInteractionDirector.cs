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
	public override void _Ready(){
		base._Ready();

		hitBoxDir.AreaEntered += KickoffSequence;
		hitBoxDir.AreaExited += StopSequence;
		hitBoxDir.BodyEntered += KickoffSequence;
		takeDamageTimer.Timeout += TakeAoEDamage;
	}


	//-------------------------------------------------------------------------
	// Methods
	// Public

	// Protected
	protected override void LoadInteractionData() {
		interactionData = (NPCInteractionData) GD.Load(interactionDataPath);
	}

	// Private
	private void KickoffSequence(Area3D otherArea){
		if (otherArea.Name == "Hit-Box-Director") {
			BeginAoEDamageSequence(otherArea);
		}
	}

	private void KickoffSequence(Node3D otherNode) {
		GD.Print(otherNode.Name);
		if ((string) otherNode.GetMeta("ID") == "Projectile") {
			ProjectileDamageSequence(otherNode);
		}
	}

	private void StopSequence(Area3D otherArea) {
		if (otherArea.Name == "Hit-Box-Director") {
			StopAoEDamageSequence();
		}
	}

	private void BeginAoEDamageSequence(Area3D aoeArea) {
		// Get the Attack Information
		AttackData aoeData = aoeArea.GetParent<PlayerAttackDirector>().GetAttackData();

		// Take the Initial Damage
		currentAoEDamage = aoeData.damage;
		TakeAoEDamage();

		// Begin Damage Timer
		SettakeDamageTimerProps(aoeData.delay, false);
		takeDamageTimer.Start();
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

	private void TakeAoEDamage() {
		TakeDamage(currentAoEDamage);
	}

	private void ProjectileDamageSequence(Node3D projNode) {
		// Get the projectile's damage
		float damage = ((ProjectileDir) projNode).damage;

		// Take damage from the projectile
		TakeDamage(damage);
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
