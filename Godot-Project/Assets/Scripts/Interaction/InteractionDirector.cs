using Godot;
using System;

public partial class InteractionDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public string interactionDataPath;
	[Export] public bool debugMode = false;

	// Protected
	protected Area3D hitBoxDir;
	protected CollisionShape3D  hitBoxShape;
	protected Timer takeDamageTimer;
	[Export] protected PackedScene damageLabel;
	protected float currentHealth = 0.0f;

	// Private
	private InteractionData interactionData;
	private Node mainRoot;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		mainRoot = GetTree().Root.GetChild(0);
		hitBoxDir = GetNode<Area3D>("Hit-Box-Director");
		hitBoxShape = GetNode<CollisionShape3D>("Hit-Box-Director/Hit-Box-Shape");
		takeDamageTimer = GetNode<Timer>("Take-Damage-Timer");

		LoadInteractionData();
		InitHealthData();
		
		hitBoxDir.BodyEntered += ProjectileDamageSequence; 
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public virtual InteractionData GetInteractionData() {
		return interactionData;
	}

	public void TickHealth(float damageValue) {
		currentHealth -= damageValue;

		DisplayDamageValue(damageValue);

		CheckIfDead();

		if (debugMode) {
			GD.Print($"{this} took {damageValue} damage");
			GD.Print($"current health: {currentHealth}\n");
		}
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

	protected void ProjectileDamageSequence(Node3D projNode) {
		if (projNode.Name != "Generic-Projectile") {
			return;
		}

		// Get the projectile's damage
		float damage = ((ProjectileDir) projNode).damage;

		// Take damage from the projectile
		TickHealth(damage);
	}

	protected void CheckIfDead() {
		if (currentHealth <= 0.0f) {
			DeathSequence();
		}
	}

	protected virtual void DeathSequence() {
		QueueFree();
	}

	// Private

	//-------------------------------------------------------------------------
	// Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TO DO: Do some shit
