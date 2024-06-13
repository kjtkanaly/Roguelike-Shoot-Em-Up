using Godot;
using System;

public partial class DamageLabel : Label3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private float duration = 1.0f;
    private float time = 0.0f;
    private float maxAlpha = 1.0f;
    private float alphaSlope;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        alphaSlope = -maxAlpha / duration;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        time += (float) delta;

        FadeSprite();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private
    private void FadeSprite() {
        float alpha = alphaSlope * time + maxAlpha;

        Color mod = Modulate;
        Color outMod = OutlineModulate;

        mod.A = alpha;
        outMod.A = alpha;

        Modulate = mod;
        OutlineModulate = outMod;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TO DO: Do some shit