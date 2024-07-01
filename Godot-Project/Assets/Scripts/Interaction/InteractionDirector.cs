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
		this.AddChild(damageLabelInst);

		damageLabelInst.Text = damageValue.ToString("0.00");
	}

	protected virtual void LoadInteractionData() {
		interactionData = (InteractionData) GD.Load(interactionDataPath);
	}

	protected void InitHealthData() {
		currentHealth = GetInteractionData().maxHealth;
	}

	protected void BeginAoEDamageSequence(Area3D aoeArea) {
		if (aoeArea.Name != "AoE-Hit-Box-Director") {
			return;
		}

		// Get the Attack Information
		TimeDelayedAttackData aoeData = (TimeDelayedAttackData) aoeArea.GetParent<PlayerAoEAttackDirector>().GetAttackData();

		TimeDelayedDamageSequence(aoeData);
	}

	protected void TimeDelayedDamageSequence(TimeDelayedAttackData attackData) {
		// Take the Initial Damage
		TickHealth(attackData.damage);

		// Create the new AoE object
		ActiveAoE aoe = new ActiveAoE(this, attackData);

		// Start the AoE Damage Timer
		aoe.delayTimer.Start();

		// Setup the aoe to end once out of range
		hitBoxDir.AreaExited += aoe.Destroy;
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

	//-------------------------------------------------------------------------
	// Nested Classes
	public partial class ActiveAoE : Node{
		public float damage = 0.0f;
		public Timer delayTimer;
		private InteractionDirector interDir;	

		public ActiveAoE(InteractionDirector interDirInst, TimeDelayedAttackData aoeData) {
			interDir = interDirInst;
			interDir.AddChild(this);

			damage = aoeData.damage;

			delayTimer = new Timer();
			this.AddChild(delayTimer);
			delayTimer.WaitTime = aoeData.delay;
			delayTimer.Timeout += TickInteractionDirHealth;
		}

		public void Destroy(Area3D area) {
			QueueFree();
		}

		private void TickInteractionDirHealth() {
			interDir.TickHealth(damage);
		}
	}
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TO DO: Do some shit
