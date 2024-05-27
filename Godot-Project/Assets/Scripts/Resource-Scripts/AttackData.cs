using Godot;
using System;

[GlobalClass]
public partial class AttackData : Resource
{
    public enum Type {
		None = 0,
		Projectile = 1,
		AreaOfEffect = 2,
		Melee = 3
	}

    // General Parameters
    [Export] public string id = "Default-001";
    [Export] public Type type = Type.None;
	[Export] public float damage = 1.0f;
	[Export] public float delay = 1.0f;
    [Export] public int maxLevel = 6;
}