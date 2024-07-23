using Godot;
using System;

public partial class PlayerAnimationController : AnimationController
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private PlayerMovementDirector movementDir;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected
    protected override void SetMovementDirector() {
        movementDir = GetNode<PlayerMovementDirector>("..");
    }

    protected override PlayerMovementDirector GetMovementDirector() {
        return movementDir;
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
