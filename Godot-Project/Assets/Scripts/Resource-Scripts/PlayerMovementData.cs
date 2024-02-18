using Godot;
using System;

[GlobalClass]
public partial class PlayerMovementData : Resource
{
	[Export] public float speed = 0.0f;
	[Export] public float acceleration = 0.0f;
	[Export] public float friction = 0.0f;
 	[Export] public float jumpVelocity = 0.0f;  
	[Export] public float rotationSpeed = 0.0f;
	[Export] public float rotationAcceleration = 0.0f;
	[Export] public float mouseSensitivity = 0.0f;
	[Export] public float rollSpeed = 0.0f;
    public float gravity = ProjectSettings.GetSetting(
                               "physics/3d/default_gravity").AsSingle();
}