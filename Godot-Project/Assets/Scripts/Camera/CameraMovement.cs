using Godot;
using System;

public partial class CameraMovement : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    Node3D player;
    Vector3 cameraPositionOffset;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        base._Ready();

        player = GetNode<Node3D>("../Player-Director");
        cameraPositionOffset = Position - player.Position;
    }

    public override void _PhysicsProcess(double delta) 
    {
        base._PhysicsProcess(delta);

        Position = player.Position + cameraPositionOffset;
    }

    //-------------------------------------------------------------------------
	// Methods
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}