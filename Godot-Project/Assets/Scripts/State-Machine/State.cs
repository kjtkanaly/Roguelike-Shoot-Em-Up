using Godot;
using System;

public partial class State : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public String animationPath;
    public float moveSpeed;

    // Protected
    protected CharacterDirector characterDir;
    protected AnimationPlayer animationPlayer;
    protected float gravity = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void Init(CharacterDirector characterDirRef, AnimationPlayer animationPlayerRef) {
        characterDir = characterDirRef;
        animationPlayer = animationPlayerRef;
    }

    virtual public void Enter() {
        animationPlayer.Play(animationPath);

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
