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
		GD.Print(level);

		// Instantiate the bullet
		projectileInst = (RigidBody3D) data.projectile.Instantiate();
		MainRoot.AddChild(projectileInst);
		projectileInst.GlobalPosition = GlobalPosition;

		// Apply an impulse to the projectle
		projectileInst.LinearVelocity = new Vector3(1, 0, 0) * data.projectileSpeed;
	}
}
