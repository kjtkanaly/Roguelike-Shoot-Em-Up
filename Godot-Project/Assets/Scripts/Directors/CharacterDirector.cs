using Godot;
using System;

public partial class CharacterDirector : CharacterBody3D
{
    public enum CharacterType {
        Player = 0,
        Ally = 1,
        Enemy = 2
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public String movementDataPath;
    [Export] public CharacterType charactertype;
    public float gravity = (float) ProjectSettings.GetSetting("physics/3d/default_gravity");

    // Protected
    [Export] protected AnimationPlayer animations;
    [Export] protected StateMachine movementSM;
    [Export] protected Node3D model;
    [Export] protected Area3D itemRange;
    [Export] protected Area3D itemPickup;
    protected GameStats gameStats;
    protected MovementData movementData;
    protected PlayerStats playerStats;

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        Init();
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        if (charactertype == CharacterType.Enemy)  {
            gameStats.IncrementKillCount();
        }
    }

    public void Init() {
        // Get the memeber nodes
        movementData = (MovementData) GD.Load(movementDataPath);

        // Get the Game Stats Instance
        foreach (Node node in GetTree().GetNodesInGroup("Game Stats")) {
            gameStats = (GameStats) node;
        }

        // Initialize the movement State Machine
        movementSM.Init(this, animations);

        // Init the Item Range Area
        itemRange.AreaEntered += ItemWithinRange;

        // Init the Item Pickup Area
        itemPickup.AreaEntered += PickupItem;

        // Get the global player stats object
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        movementSM.ProcessInput(inputEvent);
    }

    public override void _PhysicsProcess(double delta)
    {   
        // Call the Movement State Machine's Physics Process
        movementSM.PhysicsProcess((float) delta);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public MovementData GetMovementData() {
        return movementData;
    }

    public Node3D GetModel() {
        return model;
    }

    public void ApplyGravity(float delta) {
        float verticalSpeed = 
                Velocity.Y 
                - (movementData.mass  * gravity  * delta);
        Velocity = new Vector3(
                Velocity.X, 
                verticalSpeed, 
                Velocity.Z);
    }

    // Protected
    protected void ItemWithinRange(Area3D area) 
    {
        // Check if the item is an item
        if (!area.IsInGroup("Item")) 
        {
            return;
        }

        // Get the item's director
        StaticBodyDirector item = (StaticBodyDirector) area;

        item.FollowPlayer(this, true);
    }

    protected void PickupItem(Area3D area) 
    {
        // Check if the area is an item
        if (!area.IsInGroup("Item")) 
        {
            return;
        }

        // If the item is a candy then update the player's count
        if (area.IsInGroup("Candy")) 
        {
            playerStats.candyCount += 1;
            GD.Print($"Candy Count: {playerStats.candyCount}");
        }
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
