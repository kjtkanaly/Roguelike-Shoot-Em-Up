using Godot;
using System;

public partial class EnemyMovementDirector : NPCMovementDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private EnemyMovementData movementData;
    private Node3D playerNode;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        playerNode = GetTree().Root.GetChild(0).GetNode<Node3D>("Player-Director");
    }

    public override void _PhysicsProcess(double delta) {
        base._PhysicsProcess(delta);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void LoadMovementData() {
        movementData = (EnemyMovementData) GD.Load(movementDataPath);
    }

    public override EnemyMovementData GetMovementData() {
		return movementData;
	}

    // Protected
    protected override void UpdateLateralDirection() {
        Vector2 playerLateralPos = new Vector2(playerNode.GlobalPosition.X, 
                                               playerNode.GlobalPosition.Z);
        Vector2 lateralPos = new Vector2(GlobalPosition.X, 
                                         GlobalPosition.Z);
		lateralDirection = (playerLateralPos - lateralPos).Normalized();
	}

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
