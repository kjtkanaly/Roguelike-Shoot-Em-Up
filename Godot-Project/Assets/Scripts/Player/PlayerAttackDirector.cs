using Godot;
using System;

public partial class PlayerAttackDirector : Node3D
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Private
	private Node MainRoot;
	private bool debug = false;
	private MeshInstance3D meshInstance = null;
	private Area3D AoEHitBoxDirector = null;
	private CollisionShape3D AoEHitBox = null;
	private AttackData data = null;
	// Public
	public Timer timer = null;
	public int attackIndex = -1;
	public int level = 1;
	public AttackData.Type type = AttackData.Type.None;
	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		timer = GetAttackTimer();
		meshInstance = GetMeshInstance();
		SetAoEObjects();
		MainRoot = GetTree().Root.GetChild(0);
	}
	//-------------------------------------------------------------------------
	// Methods
	public Timer GetAttackTimer() {
		foreach (Node node in GetChildren()) {
			if (node.Name == "Attack Timer") {
				return (Timer) node;
			}
		}

		return null;
	}

	public MeshInstance3D GetMeshInstance() {
		foreach (Node node in GetChildren()) {
			if (node.Name == "Attack Mesh") {
				return (MeshInstance3D) node;
			}
		}

		return null;
	}

	public void SetAoEObjects() {
		AoEHitBoxDirector = GetNode<Area3D>("AoE-Hit-Box-Director");
		AoEHitBox = AoEHitBoxDirector.GetNode<CollisionShape3D>("AoE-Hit-Box");
	}

	public void SetAttackIndex(int index) {
		attackIndex = index;
	}

	public void SetData(AttackData dataVal) {
		data = dataVal;
	}

	public void InitTimer(Timer timerInstance) {
		timer = timerInstance;
		timer.Timeout += CallAttack;

		UpdateTimerTime();
		StartTimer();
	}

	public void SetVisuals(AttackData data) {
		if (data.type == AttackData.Type.AreaOfEffect) {
			meshInstance.Mesh = ((AreaOfEffectData) data).areaMesh;
		}
	}

	public void SetColliderInformation(AttackData data) {
		if (data.type == AttackData.Type.AreaOfEffect) {
			AoEHitBox.Shape = ((AreaOfEffectData) data).areaColliderShape;
		}
	}

	private void UpdateTimerTime() {
		timer.WaitTime = data.delay;
	}

	private void StartTimer() {
		timer.Start();
	}

	private void CallAttack() {
		if (debug) {
			GD.Print($"Attack Index {attackIndex}: Time Delay = {data.delay}s");
		}

		if (data.type == AttackData.Type.Projectile) {
			ProjectileAttackSequence((ProjectileData) data);
		} else if (data.type == AttackData.Type.AreaOfEffect) {

		} else if (data.type == AttackData.Type.Melee) {

		} else {

		}
	}

	public void LevelUpAttack() {
		level += 1;

		// Type Specefic Level Up Actions
		switch(data.type) {
			case AttackData.Type.Projectile:
				break;
			case AttackData.Type.AreaOfEffect:
				Scale += new Vector3(1, 1, 1) 
						 * ((AreaOfEffectData) data).areaIncreaseStepSize;
				break;
			case AttackData.Type.Melee:
				break;            
		}
	}

	public void ProjectileAttackSequence (ProjectileData data) {
		// Set the projectle's velocity
		Vector3[] initVels = ProjectileAttackObject.GetProjectileInitVelocities(
			1,
			level);
		
		for (int i = 0; i < level; i++) {
			// Instantiate the projectile
			ProjectileDir projectileInst = 
				(ProjectileDir) data.projectile.Instantiate();
			MainRoot.AddChild(projectileInst);
			projectileInst.SetMeta("ID", "Projectile");
			projectileInst.GlobalPosition = GlobalPosition;
			projectileInst.LinearVelocity = initVels[i] * data.projectileSpeed;
			projectileInst.damage = data.damage;
		}
	}

	//-------------------------------------------------------------------------
	// Debug Methods
}

