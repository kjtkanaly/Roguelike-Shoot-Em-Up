using Godot;
using System;

[GlobalClass]
public partial class PlayerMovementData : MovementData
{
 	[Export] public float jumpVelocity = 0.0f;  
	[Export] public float rotationSpeed = 0.0f;
	[Export] public float rotationAcceleration = 0.0f;
	[Export] public float mouseSensitivity = 0.0f;
	[Export] public float rollSpeed = 0.0f;
}