using Godot;
using System;

public partial class NPCInteractionDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private Area3D HitBoxDirector = null;
	private Timer DamageTimer = null;
	private float currentAoEDamage = 0.0f;

	// Public

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready(){
		HitBoxDirector = GetNode<Area3D>("NPC-Hit-Box-Director");
		HitBoxDirector.AreaEntered += KickoffSequence;
		HitBoxDirector.AreaExited += StopSequence;
		HitBoxDirector.BodyEntered += KickoffSequence;

		DamageTimer = GetNode<Timer>("Damage-Timer");
		DamageTimer.Timeout += TakeAoEDamage;
	}


	//-------------------------------------------------------------------------
	// Methods
	private void KickoffSequence(Area3D otherArea){
		if (otherArea.Name == "AoE-Hit-Box-Director") {
			BeginAoEDamageSequence(otherArea);
		}
	}

	private void KickoffSequence(Node3D otherNode) {
		if ((string) otherNode.GetMeta("ID") == "Projectile") {
			ProjectileDamageSequence(otherNode);
		}
	}

	private void StopSequence(Area3D otherArea) {
		if (otherArea.Name == "AoE-Hit-Box-Director") {
			StopAoEDamageSequence();
		}
	}

	private void BeginAoEDamageSequence(Area3D aoeArea) {
		// Get the Attack Information
		PlayerAttackData aoeData = aoeArea.GetParent<PlayerAttackObject>().data;

		// Take the Initial Damage
		currentAoEDamage = aoeData.damage;
		TakeAoEDamage();

		// Begin Damage Timer
		SetDamageTimerProps(aoeData.delay, false);
		DamageTimer.Start();
	}

	private void StopAoEDamageSequence() {
		// Reset currentAoEDamage
		currentAoEDamage = 0.0f;

		// Stop Damage TImer
		DamageTimer.Stop();
	}

	private void SetDamageTimerProps(float waitTimeVal, bool oneShotVal) {
		DamageTimer.WaitTime = waitTimeVal;
		DamageTimer.OneShot = oneShotVal;
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
