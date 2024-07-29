using Godot;
using System;

[GlobalClass]
public partial class InteractionData : Resource
{
    [Export] public bool canTakeDamage = true;
    [Export] public float maxHealth = 1;
    [Export] public string groupName = "";
    [Export] public float deathAnimationTIme = 1f;
}