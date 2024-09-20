using Godot;
using System;

public partial class InteractionDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Custom Signals
	[Signal]
    public delegate void TookDamageEventHandler(AttackData data);
	[Signal]
	public delegate void HasDiedEventHandler();

	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public string interactionDataPath;
	[Export] public bool debugMode = false;

	// Protected
	protected ObjectPickupDirector itemPickupDir;
	protected InventoryDirector inventoryDir;
	[Export] protected PackedScene damageLabel;
	[Export] protected CharacterDirector characterDir;
	protected float currentHealth = 0.0f;
	protected Godot.SceneTreeTimer deathTimer;
    [Export] protected AudioStreamPlayer soundFx;

	// Private
	private InteractionData interactionData;
	private Node mainRoot;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		mainRoot = GetTree().Root.GetChild(0);

		LoadInteractionData();
		InitHealthData();

		itemPickupDir = GetNode<ObjectPickupDirector>("Item-Pickup-Director");
        inventoryDir = GetNode<InventoryDirector>("Inventory-Director");
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public virtual InteractionData GetInteractionData() {
		return interactionData;
	}

	public void TickHealth(AttackData data) {
		if (IsDead()) {
			return;
		}

		currentHealth -= data.damage;

		DisplayDamageValue(data.damage);
		EmitSignal(SignalName.TookDamage, data);

        soundFx.Play();

		if (IsDead()) {
			BeginDeathSequence();
		}

		if (debugMode) {
			GD.Print($"{this} took {data.damage} damage");
			GD.Print($"current health: {currentHealth}\n");
		}
	}

	public bool IsDead() {
		if (currentHealth <= 0.0f) {
			return true;
		}
		return false;
	}

	public float GetCurrentHealth() {
		return currentHealth;
	}

	// Protected
	protected virtual void DisplayDamageValue(float damageValue) {
		Label3D damageLabelInst = 
			(Label3D) damageLabel.Instantiate();
		mainRoot.AddChild(damageLabelInst);

		damageLabelInst.Text = damageValue.ToString("0.00");
		damageLabelInst.GlobalPosition = 
			new Vector3(GlobalPosition.X, 
						damageLabelInst.GlobalPosition.Y, 
						GlobalPosition.Z);
	}

	protected virtual void LoadInteractionData() {
		interactionData = (InteractionData) GD.Load(interactionDataPath);
	}

	protected void InitHealthData() {
		currentHealth = GetInteractionData().maxHealth;
	}

	protected virtual void PickupFirstFreeAttack() {
    }

	protected virtual void BeginDeathSequence() {
		GD.Print($"\n{characterDir.Name} has died\n");
		EmitSignal(SignalName.HasDied);
		inventoryDir.DisableAttacks();
		deathTimer = 
			GetTree().CreateTimer(GetInteractionData().deathAnimationTIme);
		deathTimer.Timeout += EndDeathSequence;
	}

	protected virtual void EndDeathSequence() {
		characterDir.QueueFree();
	}

	// Private

	//-------------------------------------------------------------------------
	// Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TO DO: Do some shit
