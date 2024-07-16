using Godot;
using System;

public partial class DamageLabel : Label3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private Timer durationTimer;
    [Export] private float vInit = 0.2f;
    [Export] private float mass = 0.1f;
    [Export] private float duration = 1.0f;
    private float time = 0.0f;
    private float maxAlpha = 1.0f;
    private float alphaSlope;
    private float gravity = ProjectSettings.GetSetting(
						    "physics/3d/default_gravity").AsSingle();

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        InitDurationTimer();

        alphaSlope = -maxAlpha / duration;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        time += (float) delta;

        FadeSprite();
        AnimateText();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private
    private void InitDurationTimer() {
        durationTimer = GetNode<Timer>("Duration-Timer");
        durationTimer.WaitTime = duration;
        durationTimer.Start();
        durationTimer.Timeout += QueueFree;
    }

    private void FadeSprite() {
        float alpha = alphaSlope * time + maxAlpha;

        Color mod = Modulate;
        Color outMod = OutlineModulate;

        mod.A = alpha;
        outMod.A = alpha;

        Modulate = mod;
        OutlineModulate = outMod;
    }

    private void AnimateText() {
        Vector3 pos = Position;

        float y = pos.Y 
                  + (vInit * time) 
                  - (0.5f * mass * gravity * Mathf.Pow(time, 2));

        pos.Y = y;
        Position = pos;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
