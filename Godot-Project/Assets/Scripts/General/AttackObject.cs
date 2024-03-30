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
	private Node MainRoot = null;
	private Node3D projectileInst = null;

	public AttackObject(PlayerAttackData dataVal) {
		data = dataVal;        
	}

	public AttackObject() {
	}

	public void SetMainRootVar() {
		MainRoot = GetTree().Root.GetChild(0);
	}

	public void ProjectileAttackSequence() {
		// Instantiate the bullet
		projectileInst = (Node3D) data.projectile.Instantiate();
		MainRoot.AddChild(projectileInst);
		projectileInst.GlobalPosition = GlobalPosition;
	}
}
