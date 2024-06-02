using Godot;
using System;

public partial class NPCInteractionDirector : InteractionDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private float currentAoEDamage = 0.0f;

	// Public

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready(){
		base._Ready();

		HitBoxDir.AreaEntered += KickoffSequence;
		HitBoxDir.AreaExited += StopSequence;
		HitBoxDir.BodyEntered += KickoffSequence;
		TakeDamageTimer.Timeout += TakeAoEDamage;
	}


	//-------------------------------------------------------------------------
	// Methods
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
		SetTakeDamageTimerProps(aoeData.delay, false);
		TakeDamageTimer.Start();
	}

	private void StopAoEDamageSequence() {
		// Reset currentAoEDamage
		currentAoEDamage = 0.0f;

		// Stop Damage TImer
		TakeDamageTimer.Stop();
	}

	private void SetTakeDamageTimerProps(float waitTimeVal, bool oneShotVal) {
		TakeDamageTimer.WaitTime = waitTimeVal;
		TakeDamageTimer.OneShot = oneShotVal;
	}

	private void TakeAoEDamage() {
		TakeDamage(currentAoEDamage);
	}

	public void TakeDamage(float damageValue) {
		GD.Print($"Enemey took {damageValue} damage");
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
