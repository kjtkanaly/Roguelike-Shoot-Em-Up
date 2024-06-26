using Godot;
using System;

public partial class EnemeyInteractionDirector : NPCInteractionDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public

	// Protected

	// Private
	private EnemyInteractionData interactionData;
	private Node parentNode;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		GetParentNode();

		if (debugMode) {
			GD.Print($"Enemy Node: {Name}");
			GD.Print($"Parent Node: {parentNode}");
			GD.Print($"Current Health: {currentHealth}\n");
		}
	}

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override EnemyInteractionData GetInteractionData() {
		return interactionData;
	}

	// Protected
	protected override void LoadInteractionData() {
		interactionData = (EnemyInteractionData) GD.Load(interactionDataPath);
	}

	protected override void DeathSequence() {
		parentNode.QueueFree();
	}

	// Private
	private void GetParentNode() {
		Node currentNode = (Node) this;

		while (currentNode != null) {
			parentNode = currentNode.GetParent();

			if (parentNode.IsInGroup("Enemy-Parent-Node")) {
				return;
			}

			currentNode = parentNode;
		}

		parentNode = null;
		return;
	}

	//-------------------------------------------------------------------------
	// Debug Methods

}
