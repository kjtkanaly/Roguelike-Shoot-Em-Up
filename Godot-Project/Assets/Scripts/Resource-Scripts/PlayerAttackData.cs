using Godot;
using System;

[GlobalClass]
public partial class AttackData : Resource
{
    // General Parameters
    [Export] public string id = "Default-001";
    [Export] public AttackObject.Type type = AttackObject.Type.None;
	[Export] public float damage = 1.0f;
	[Export] public float delay = 1.0f;
    [Export] public int maxLevel = 6;
}