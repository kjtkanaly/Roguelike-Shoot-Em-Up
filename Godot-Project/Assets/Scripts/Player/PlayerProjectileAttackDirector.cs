using Godot;
using System;

public partial class PlayerProjectileAttackDirector : PlayerAttackDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	[Export] private ProjectileData data = null;

	//-------------------------------------------------------------------------
	// Game Events

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public override void SetData(AttackData dataVal) {
		data = (ProjectileData) dataVal;
	}

	public override AttackData GetAttackData() {
		return data;
	}
	
	public override void LevelUpAttack() {
		GD.Print("Level Up Projectile");
		level += 1;
	}

	public override string GetAttackId() {
		return data.id;
	}

	public override int GetAttackMaxLevel() {
		return data.maxLevel;
	}

	// Protected
	protected override void UpdateTimerTime() {
		timer.WaitTime = data.delay;
	}

	protected override void CallAttack() {
		if (debug) {
			GD.Print($"Projectile Attack:");
			GD.Print($"Index {attackIndex} | Time Delay = {data.delay}s");
		}

		ProjectileSequence();
	}

	// Private 
	private void ProjectileSequence () {
		Vector3[] initVels = GetProjectileInitVelocities(
								 level, 
								 data.initVelAngleOffsetDeg);

		for (int i = 0; i < level; i++) {
			// Instantiate the projectile
			Node3D projectileInstParent = 
				(Node3D) data.projectileObject.Instantiate();
			MainRoot.AddChild(projectileInstParent);

			ProjectileDir projectileInst = 
				(ProjectileDir) projectileInstParent.GetChild(0);			
			projectileInst.SetMeta("ID", "Projectile");
			projectileInst.GlobalPosition = GlobalPosition;
			projectileInst.LinearVelocity = initVels[i] * data.projectileSpeed;
			projectileInst.damage = data.damage;
		}
	}

	private Vector3[] GetProjectileInitVelocities(int level, float angleOffsetDeg) {
		Vector3[] initVels = new Vector3[level];

		float angleStepSizeDeg = 360.0f / level;
		float angleDeg = 0.0f + angleOffsetDeg;

		for (int i = 0; i < level; i++) {
			// Get the projectile's launch velocity
			initVels[i] = new Vector3(Mathf.Cos(Mathf.DegToRad(angleDeg)),
										0,
										Mathf.Sin(Mathf.DegToRad(angleDeg)));
			angleDeg += angleStepSizeDeg;
		}
		
		return initVels;
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}
