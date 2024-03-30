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
	public PlayerAttackData data = null;
	public int level = 1;
	private Node MainRoot = null;
	private RigidBody3D projectileInst = null;

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

		GD.Print($"{data.id}:");
		for (int angleDeg = 0; angleDeg < 360; angleDeg += angleStepSizeDeg) {
			// Instantiate the bullet
			projectileInst = (RigidBody3D) data.projectile.Instantiate();
			MainRoot.AddChild(projectileInst);
			projectileInst.GlobalPosition = GlobalPosition;

			// Get the projectile's launch velocity
			Vector3 velocityNorm = new Vector3(
				Mathf.Cos(Mathf.DegToRad(angleDeg)),
				0,
				Mathf.Sin(Mathf.DegToRad(angleDeg)));

			GD.Print($"{angleDeg}: {velocityNorm}");

			// Set the projectle's velocity
			projectileInst.LinearVelocity = velocityNorm * data.projectileSpeed;
		}
	}
}
