using Godot;
using System;

[GlobalClass]
public partial class PlayerAttackData : Resource
{
    [Export] public AttackObject.Type type = AttackObject.Type.None;
	[Export] public float damage = 1.0f;
	[Export] public float delay = 1.0f;
    [Export] public PackedScene projectile;
    [Export] public float projectileSpeed = 1.0f;
}