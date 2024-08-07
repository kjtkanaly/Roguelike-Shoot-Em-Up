using Godot;
using System;

public partial class UI : Control
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
    public void ToggleVisible(bool isVisible) {
        if (isVisible) {
            Show();
        } else {
            Hide();
        }
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}