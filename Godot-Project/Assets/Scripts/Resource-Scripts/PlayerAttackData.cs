using Godot;
using System;

[GlobalClass]
public partial class PlayerAttackData : Resource
{
    // General Parameters
    [Export] public string id = "Default-001";
    [Export] public AttackObject.Type type = AttackObject.Type.None;
	[Export] public float damage = 1.0f;
	[Export] public float delay = 1.0f;
    [Export] public int maxLevel = 6;
    // Projectile Parameters
    [Export] public PackedScene projectile;
    [Export] public float projectileSpeed = 1.0f;
    // Area of Effect Parameters
    [Export] public Mesh areaMesh;
    [Export] public float lvlUpAreaStepSize = 0.2f;
}