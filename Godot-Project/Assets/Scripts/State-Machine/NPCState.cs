using Godot;
using System;

public partial class NPCState : State
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float chaseRange = 20;

    // Protected
    protected Node3D playerDir;

    // Private

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Init(CharacterDirector characterDirRef, AnimationPlayer animationPlayerRef)
    {
        base.Init(characterDirRef, animationPlayerRef);
        GetPlayerNode();
    }

    // Protected
    public void GetPlayerNode() {
        foreach (Node node in GetTree().GetNodesInGroup("Player")){
            if (node.IsInGroup("Director")) {
                playerDir = (Node3D) node;
                break;
            }
        }
    }

    protected bool IsPlayerInChaseRange() {
        return GetDistanceToPlayer() <= chaseRange;    
    }

    protected float GetDistanceToPlayer() {
        if (playerDir == null) {
            return Mathf.Inf;
        }

        Vector2 playerLateralPos = new Vector2(playerDir.GlobalPosition.X, 
                                               playerDir.GlobalPosition.Z);
        Vector2 lateralPos = new Vector2(characterDir.GlobalPosition.X, 
                                         characterDir.GlobalPosition.Z);
        return (playerLateralPos - lateralPos).Length();
    }

    protected Vector2 GetLateralDirectionToPlayer() {
        if (playerDir == null) {
            return Vector2.Zero;
        }

        Vector2 playerLateralPos = new Vector2(playerDir.GlobalPosition.X, 
                                               playerDir.GlobalPosition.Z);
        Vector2 lateralPos = new Vector2(characterDir.GlobalPosition.X, 
                                         characterDir.GlobalPosition.Z);
		return (playerLateralPos - lateralPos).Normalized();
    }

    protected Vector2 GetLateralDirectionToPoint(Vector2 point) {
        if (playerDir == null) {
            return Vector2.Zero;
        }

        Vector2 lateralPos = new Vector2(characterDir.GlobalPosition.X, 
                                         characterDir.GlobalPosition.Z);
		return (point - lateralPos).Normalized();
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}