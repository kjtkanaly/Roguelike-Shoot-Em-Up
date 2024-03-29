using Godot;
using System;
using System.Collections.Generic;

public class AttackObject 
{
    public enum Type {
        None = 0,
        Projectile = 1,
        AreaOfEffect = 2,
        Melee = 3
    }
    [Export] public float damage = 1.0f;
	[Export] public float delay = 1.0f;

	public AttackObject(float damageVal, float delayVal) {
        damage = damageVal;
		delay = delayVal;
	}
}