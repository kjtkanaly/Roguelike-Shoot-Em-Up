using Godot;
using System;

public partial class NPCAnimationController : AnimationController
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private NPCMovementDirector movementDir;

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
        movementDir = GetNode<NPCMovementDirector>("..");
    }

    protected override NPCMovementDirector GetMovementDirector() {
        return movementDir;
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
