using Godot;
using System;

public partial class StaticBodyDirector : Area3D
{
    public enum ItemType 
    {
        Candy = 0,
        Attack = 1,
        Unkown = -1
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public ItemType itemType = ItemType.Unkown;
    public bool followPlayerFlag = false;

    // Protected
    [Export] protected StateMachine stateMachine;
    [Export] protected AnimationPlayer animationPlayer;
    [Export] protected AudioStreamPlayer soundFx;
    [Export] protected CharacterDirector charDir;

    // Private

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void _Ready()
    {
        Init();
    }

    public void Init() {
        // Initialize the movement State Machine
        stateMachine.Init(this, animationPlayer, soundFx);
    }

    public override void _PhysicsProcess(double delta)
    {   
        // Call the Movement State Machine's Physics Process
        stateMachine.PhysicsProcess((float) delta);
    }

    public void FollowPlayer(CharacterDirector character, bool state) 
    {
        charDir = character;
        followPlayerFlag = state;
    }

    public CharacterDirector GetCharacterDir() 
    {
        return charDir;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
