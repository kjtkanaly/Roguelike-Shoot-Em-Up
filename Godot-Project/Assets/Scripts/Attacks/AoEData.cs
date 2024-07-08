using Godot;
using System;

[GlobalClass]
public partial class AreaOfEffectData : RepetativeAttackData
{
    // Area of Effect Parameters
    [Export] public Mesh areaMesh;
    [Export] public Shape3D areaColliderShape;
    [Export] public float areaIncreaseStepSize = 0.2f;
}