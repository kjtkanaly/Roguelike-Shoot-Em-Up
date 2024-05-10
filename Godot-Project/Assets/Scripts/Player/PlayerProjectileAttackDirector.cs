using Godot;
using System;

public partial class PlayerProjectileAttackDirector : PlayerAttackDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private ProjectileData data = null;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public override void SetData(AttackData dataVal) {
		data = (ProjectileData) dataVal;
	}

    public override void LevelUpAttack() {
		level += 1;
	}

    // Protected
    protected override void CallAttack() {
		if (debug) {
			GD.Print($"Projectile Attack:");
            GD.Print($"Index {attackIndex} | Time Delay = {data.delay}s");
		}

        ProjectileSequence(data);
	}

    // Private 
    private void ProjectileSequence (ProjectileData data) {
        Vector3[] initVels = GetProjectileInitVelocities(
                                 level, 
                                 data.initVelAngleOffsetDeg);

		for (int i = 0; i < level; i++) {
			// Instantiate the projectile
			ProjectileDir projectileInst = 
                (ProjectileDir) data.projectileObject.Instantiate();
			MainRoot.AddChild(projectileInst);
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