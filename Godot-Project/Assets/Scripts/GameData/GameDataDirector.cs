using Godot;
using System;
using System.Collections.Generic;

public partial class GameDataDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Game Events
    public GameDataDirector() {

    }

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void SaveGameState(Godot.Collections.Array<Godot.Node> persitNodes) {
        FileAccess saveFile = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);

        // Iterate over all of the persit nodes in the scene
        foreach (PersistNode persitNode in persitNodes)
        {
            // Check the node is an instanced scene so it can be instanced again during load.
            if (string.IsNullOrEmpty(persitNode.SceneFilePath))
            {
                GD.Print($"persistent node '{persitNode.Name}' is not an instanced scene, skipped");
                continue;
            }

            // Get the serialized data for the persitNode
            Godot.Collections.Dictionary<string, Variant> serialData = 
                persitNode.Serialize();

            // Json provides a static method to serialized JSON string.
            var jsonString = Json.Stringify(serialData);

            // Store the save dictionary as a new line in the save file.
            saveFile.StoreLine(jsonString);

            // Debug 
            foreach(KeyValuePair<string, Variant> entry in serialData) {
                GD.Print($"{entry.Key}: {entry.Value}");
            }
        }

        saveFile.Close();
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
