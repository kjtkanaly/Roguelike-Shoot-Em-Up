using Godot;
using System;

public partial class IdleItem : StaticBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] float freq = 1.0f;
    [Export] float followRange = 5.0f;
    [Export] public Vector2 easeRange = new Vector2(0.4f, 0.6f);

    // Protected
    [Export] protected State followPlayer;
    protected float yIntercept = 0.5f;
    protected float time = 0.0f;

    // Private

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        base.Enter();

        yIntercept = staticBodyDir.Position.Y;
    }

    public override void Exit() 
    {
        GD.Print("Test");
    }

    override public State ProcessPhysics(float delta) 
    {
        // Check if the item is within follow range of the player
        if (staticBodyDir.followPlayerFlag) {
            return followPlayer;
        }

        Vector3 newPos = staticBodyDir.Position;
        time += delta * freq;
        
        
        newPos.Y = EaseInOutSine(time) 
                   * (easeRange.Y - easeRange.X) 
                   + easeRange.X 
                   + yIntercept;
        staticBodyDir.Position = newPos;

        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
