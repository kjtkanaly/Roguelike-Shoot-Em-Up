using Godot;
using System;
using System.Collections.Generic;

public partial class AttackObject : Node3D
{
	public enum Type {
		None = 0,
		Projectile = 1,
		AreaOfEffect = 2,
		Melee = 3
	}
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private Node MainRoot = null;
	private ProjectileDir projectileInst = null;
	private RigidBody3D projectileInstRB = null;
	// Public
	public PlayerAttackData data = null;
	public int level = 1;
	//-------------------------------------------------------------------------
	// Game Events
	
	//-------------------------------------------------------------------------
	// Methods
	public AttackObject(PlayerAttackData dataVal) {
		data = dataVal;        
	}

	public AttackObject() {
	}

	public void SetMainRootVar() {
		MainRoot = GetTree().Root.GetChild(0);
	}

	public void ProjectileAttackSequence() {
		int angleStepSizeDeg = 360 / level;

		for (int angleDeg = 0; angleDeg < 360; angleDeg += angleStepSizeDeg) {
			// Instantiate the bullet
			projectileInst = (ProjectileDir) data.projectile.Instantiate();
			MainRoot.AddChild(projectileInst);
			projectileInst.SetMeta("ID", "Projectile");

			// 
			projectileInstRB = projectileInst;
			projectileInstRB.GlobalPosition = GlobalPosition;

			// Get the projectile's launch velocity
			Vector3 velocityNorm = new Vector3(
				Mathf.Cos(Mathf.DegToRad(angleDeg)),
				0,
				Mathf.Sin(Mathf.DegToRad(angleDeg)));

			// Set the projectle's velocity
			projectileInstRB.LinearVelocity = velocityNorm * data.projectileSpeed;

			// Set the porjectile's damage
			projectileInst.damage = data.damage;
		}
	}
}
