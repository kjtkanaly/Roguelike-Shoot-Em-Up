using Godot;
using System;

// The PersistNode will exist as a composit of the character director
public partial class PersistNode : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public virtual Godot.Collections.Dictionary<string, Variant> Serialize()
    {
        // Init the Dict
        Godot.Collections.Dictionary<string, Variant> dict = 
            new Godot.Collections.Dictionary<string, Variant>(){};
        
        dict.Add("Filename", GetParent().SceneFilePath);

        // Return the Dictionary
        return dict;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
