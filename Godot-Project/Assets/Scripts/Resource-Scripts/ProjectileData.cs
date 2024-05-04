using Godot;
using System;

[GlobalClass]
public partial class ProjectileData : PlayerAttackData
{
    // Projectile Parameters
    [Export] public PackedScene projectile;
    [Export] public float projectileSpeed = 1.0f;
}