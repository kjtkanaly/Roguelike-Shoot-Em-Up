using Godot;
using System;

public partial class PlayerAoEAttackDirector : PlayerAttackDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    
    // Protected

    // Private
    [Export] private AreaOfEffectData data = null;
    private Area3D AoEHitBoxDirector = null;
	private CollisionShape3D AoEHitBox = null;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        timer = GetAttackTimer();
		meshInstance = GetMeshInstance();
		MainRoot = GetTree().Root.GetChild(0);

        // Init the attack's timer
		InitTimer();

        SetAoEObjects();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void SetData(AttackData dataVal) {
		data = (AreaOfEffectData) dataVal;
	}

    public override AttackData GetAttackData() {
		return data;
	}

    public override void SetVisuals() {
        meshInstance.Mesh = data.areaMesh;
	}

	public override void SetColliderInformation() {
        AoEHitBox.Shape = data.areaColliderShape;
	}

    public override void LevelUpAttack() {
        GD.Print("Level Up AoE");

		level += 1;

        Scale += new Vector3(1, 1, 1) 
				 * ((AreaOfEffectData) data).areaIncreaseStepSize;
	}

    public override string GetAttackId() {
		return data.id;
	}

    public override int GetAttackMaxLevel() {
		return data.maxLevel;
	}

    // Protected
    protected override void UpdateTimerTime() {
		timer.WaitTime = data.delay;
	}

    protected override void CallAttack() {
		if (debug) {
			GD.Print($"AoE Attack:");
            GD.Print($"Index {attackIndex} | Time Delay = {data.delay}s");
		}

        // Call AoE Attack Sequence
	}

    // Private
    private void SetAoEObjects() {
		AoEHitBoxDirector = GetNode<Area3D>("AoE-Hit-Box-Director");
		AoEHitBox = AoEHitBoxDirector.GetNode<CollisionShape3D>("AoE-Hit-Box");
	}
}