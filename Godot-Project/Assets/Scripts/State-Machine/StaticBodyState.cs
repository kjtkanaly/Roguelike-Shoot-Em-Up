using Godot;
using System;

public partial class StaticBodyState : State
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    protected StaticBodyDirector staticBodyDir;

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    override public void Init(StaticBodyDirector bodyDirectorRef, AnimationPlayer animationRef) 
    {
        base.Init(bodyDirectorRef, animationRef);
        staticBodyDir = bodyDirectorRef;
    }

    // Protected
    protected float EaseInOutSine(float x) 
    {
        return -(Mathf.Cos(Mathf.Pi * x) - 1) / 2;
    }

    protected float EaseInOutCubic(float x)
    {
        if (x < 0.5) {
            return 4 * x * x * x;
        }
        else {
            return 1 - (MathF.Pow(-2 * x + 2, 3) / 2);
        }
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}