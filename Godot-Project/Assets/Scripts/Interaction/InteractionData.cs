using Godot;
using System;

[GlobalClass]
public partial class InteractionData : Resource
{
    [Export] public float maxHealth = 1;
    public float currentHealth = 1;
}