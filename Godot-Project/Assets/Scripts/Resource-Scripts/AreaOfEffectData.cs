using Godot;
using System;

[GlobalClass]
public partial class AreaOfEffectData : AttackData
{
    // Area of Effect Parameters
    [Export] public Mesh areaMesh;
    [Export] public Shape3D areaColliderShape;
    [Export] public float lvlUpAreaStepSize = 0.2f;
}