using Godot;
using System;

[GlobalClass]
public partial class ProjectileData : AttackData
{
    // Projectile Parameters
    [Export] public PackedScene projectile;
    [Export] public float projectileSpeed = 1.0f;
    [Export] public int projectileIndex = 1;
    [Export] public Mesh projectileMesh = null;
}