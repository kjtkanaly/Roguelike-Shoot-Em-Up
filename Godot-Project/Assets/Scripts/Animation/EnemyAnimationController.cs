using Godot;
using System;

public partial class EnemyAnimationController : NPCAnimationController
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private EnemyMovementDirector movementDir;

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
        movementDir = GetNode<EnemyMovementDirector>("..");
    }

    protected override EnemyMovementDirector GetMovementDirector() {
        return movementDir;
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
