using Godot;
using System;

public partial class PlayerPersistNode : PersistNode
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] string playerMovementDirPath;
    [Export] string playerInteractionDirPath;

    // Protected

    // Private
    private CharacterDirector playerMovementDir;
    private PlayerInteractionDirector playerInteractionDir;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        playerMovementDir = GetNode<CharacterDirector>(playerMovementDirPath);
        playerInteractionDir = GetNode<PlayerInteractionDirector>(playerInteractionDirPath);

        base._Ready();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override Godot.Collections.Dictionary<string, Variant> Serialize()
    {
        Godot.Collections.Dictionary<string, Variant> dict = base.Serialize();

        dict.Add("PosX", playerMovementDir.Position.X);
        dict.Add("PosY", playerMovementDir.Position.Y);
        dict.Add("PosZ", playerMovementDir.Position.Z);
        dict.Add("Health", playerInteractionDir.GetCurrentHealth());

        // Return the Dictionary
        return dict;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
