using Godot;
using System;

public partial class PlayerInteractionDirector : InteractionDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public int maxAttackCount = 6;

	// Protected
	protected PlayerInventoryDirector inventoryDir = null;
	protected PlayerObjectPickup pickupArea = null;

	// Private
	private PlayerInteractionData interactionData;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();

		pickupArea = GetNode<PlayerObjectPickup>("../Item-Pickup-Director");
		inventoryDir = new PlayerInventoryDirector(maxAttackCount, this);
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public int IsActionAlreadyEquipped(string itemName) {
		return inventoryDir.IsActionAlreadyEquipped(itemName);
	}

	public bool IsPlayerMaxedOutOnAttacks() {
		return inventoryDir.IsPlayerMaxedOutOnAttacks();
	}

	public bool LevelUpEquippedAction(int itemIndex) {
		return inventoryDir.LevelUpEquippedAction(itemIndex);
	}

	public void EquipNewAttack(PackedScene newAttack) {
		inventoryDir.EquipNewAttack(newAttack);
	}

	// Protected
	protected override void LoadInteractionData() {
		interactionData = (PlayerInteractionData) GD.Load(interactionDataPath);
	}

	// Private

	//-------------------------------------------------------------------------
	// Debug Methods
}
