using Godot;
using System;

public partial class InteractionDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	[Export] public string interactionDataPath;
	[Export] public bool debugMode = true;

	// Protected
	protected Area3D hitBoxDir;
	protected CollisionShape3D  hitBoxShape;
	protected Timer takeDamageTimer;

	// Private
	private InteractionData interactionData;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		hitBoxDir = GetNode<Area3D>("Hit-Box-Director");
		hitBoxShape = GetNode<CollisionShape3D>("Hit-Box-Director/Hit-Box-Shape");
		takeDamageTimer = GetNode<Timer>("Take-Damage-Timer");

		LoadInteractionData();
		InitHealthData();
		
		hitBoxDir.AreaEntered += BeginAoEDamageSequence;
		hitBoxDir.BodyEntered += ProjectileDamageSequence; 
	}

	//-------------------------------------------------------------------------
	// Methods
	// Public
	public virtual InteractionData GetInteractionData() {
		return interactionData;
	}

	public void TickHealth(float damageValue) {
		GetInteractionData().currentHealth -= damageValue;

		if (debugMode) {
			GD.Print($"{this.Name} took {damageValue} damage");
			GD.Print($"current health: {GetInteractionData().currentHealth}\n");
		}
	}

	// Protected
	protected virtual void LoadInteractionData() {
		interactionData = (InteractionData) GD.Load(interactionDataPath);
	}

	protected virtual void InitHealthData() {
		GetInteractionData().currentHealth = GetInteractionData().maxHealth;
	}

	protected void BeginAoEDamageSequence(Area3D aoeArea) {
		if (aoeArea.Name != "AoE-Hit-Box-Director") {
			return;
		}

		// Get the Attack Information
		AreaOfEffectData aoeData = aoeArea.GetParent<PlayerAoEAttackDirector>().GetAttackData();

		// Take the Initial Damage
		TickHealth(aoeData.damage);

		// Create the new AoE object
		ActiveAoE aoe = new ActiveAoE(this, aoeData);

		// Setup the aoe to end once out of range
		hitBoxDir.AreaExited += aoe.Destroy;
	}

	private void ProjectileDamageSequence(Node3D projNode) {
		if ((string) projNode.GetMeta("ID") != "Projectile") {
			return;
		}

		// Get the projectile's damage
		float damage = ((ProjectileDir) projNode).damage;

		// Take damage from the projectile
		TickHealth(damage);
	}

	// Private

	//-------------------------------------------------------------------------
	// Debug Methods

	//-------------------------------------------------------------------------
	// Nested Classes
	public partial class ActiveAoE : Node{
		public float damage = 0.0f;
		public Timer delayTimer;
		private InteractionDirector interDir;	

		public ActiveAoE(InteractionDirector interDirInst, AreaOfEffectData aoeData) {
			interDir = interDirInst;

			damage = aoeData.damage;

			delayTimer = new Timer();
			delayTimer.WaitTime = aoeData.delay;
			delayTimer.Timeout += Tick;
			delayTimer.Start();
		}

		public void Destroy(Area3D area) {
			this.Dispose();
		}

		private void Tick() {
			interDir.TickHealth(damage);
		}
	}
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TO DO: Do some shit
