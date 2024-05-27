using Godot;
using System;

public partial class PlayerAoEAttackDirector : PlayerAttackDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    
    // Protected

    // Private
    private AreaOfEffectData data = null;
    private Area3D AoEHitBoxDirector = null;
	private CollisionShape3D AoEHitBox = null;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        SetAoEObjects();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void LoadAttackDataFile() {
		data = (AreaOfEffectData) GD.Load(dataPath);
	}

    public override AreaOfEffectData GetAttackData() {
		return data;
	}

    public override void SetVisuals() {
        meshInstance.Mesh = data.areaMesh;
	}

	public override void SetColliderInformation() {
        AoEHitBox.Shape = data.areaColliderShape;
	}

    public override void LevelUpAttack() {
        base.LevelUpAttack();

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
            GD.Print($"Time Delay = {data.delay}s");
		}

        // Call AoE Attack Sequence
	}

    // Private
    private void SetAoEObjects() {
		AoEHitBoxDirector = GetNode<Area3D>("AoE-Hit-Box-Director");
		AoEHitBox = AoEHitBoxDirector.GetNode<CollisionShape3D>("AoE-Hit-Box");
	}
}