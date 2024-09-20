using Godot;
using System;

public partial class State : Node
{
    public enum StateType 
    {
        CharacterBody = 0,
        StaticBody = 1
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public StateType type;
    [Export] public String animationPath;
    [Export] public AudioStream soundFxStream;

    // Protected
    protected AnimationPlayer animationPlayer;
    protected AudioStreamPlayer soundFx;
    protected Main global;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    virtual public void Init(CharacterDirector bodyDirectorRef, AnimationPlayer animationRef) {
        animationPlayer = animationRef;
        global = GetNode<Main>("/root/Main");
    }

    virtual public void Init(StaticBodyDirector bodyDirectorRef, AnimationPlayer animationRef, AudioStreamPlayer sfxRef) {
        animationPlayer = animationRef;
        soundFx = sfxRef;
        global = GetNode<Main>("/root/Main");
    }

    virtual public void Enter() {
        if (!string.IsNullOrEmpty(animationPath)) {
            animationPlayer.Play(animationPath);
        }

        if (soundFx != null) {
            soundFx.Stream = soundFxStream;
            soundFx.Play();
        }

        return;
    }

    virtual public void Exit() {
        return;
    }

    virtual public State ProcessInput(InputEvent inputEvent) {
        return null;
    }

    virtual public State ProcessPhysics(float delta) {
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
