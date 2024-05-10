using Godot;
using System;

public static class ProjectileAttackObject
{
    //-------------------------------------------------------------------------
	// Methods
    public static Vector3[] GetProjectileInitVelocities(int projIndex, int level) {
        Vector3[] initVels = new Vector3[level];

        switch (projIndex) {
        case (1):
            int angleStepSizeDeg = 360 / level;
            int angleDeg = 0;
            for (int i = 0; i < level; i++) {
                // Get the projectile's launch velocity
                initVels[i] = new Vector3(Mathf.Cos(Mathf.DegToRad(angleDeg)),
                                          0,
                                          Mathf.Sin(Mathf.DegToRad(angleDeg)));
                angleDeg += angleStepSizeDeg;
            }
            break;
        default:
    
            break;
        }

        return initVels
	}

    //-------------------------------------------------------------------------
	// Debug Methods
}