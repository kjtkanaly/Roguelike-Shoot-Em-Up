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

    // Private
    private CharacterDirector characterDir;
    private AnimationPlayer animationPlayer;
    private float gravity = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void Init(CharacterDirector characterDirRef, AnimationPlayer animationPlayerRef) {
        characterDir = characterDirRef;
        animationPlayer = animationPlayerRef;
    }

    public void Enter() {
        animationPlayer.Play(animationPath);

        return;
    }

    public void Exit() {
        return;
    }

    public void PhysicsProcess() {
        return;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
