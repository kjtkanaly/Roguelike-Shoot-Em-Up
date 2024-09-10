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
    protected float EaseInOutSine(float x, float f, float b) 
    {
        return (-(Mathf.Cos(f * Mathf.Pi * x) - 1) / 2) + b;
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}