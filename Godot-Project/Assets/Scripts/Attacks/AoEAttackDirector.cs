using Godot;
using System;

public partial class AoEAttackDirector : RepetativeAttackDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    
    // Protected

    // Private
    private AreaOfEffectData data = null;

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
        attackMesh.Mesh = data.areaMesh;
	}

	public override void SetColliderInformation() {
        hitBoxShape.Shape = data.areaColliderShape;
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
    protected override void CallAttack() {
		if (debug) {
			GD.Print($"AoE Attack:");
            GD.Print($"Time Delay = {data.delay}s");
		}

        // Call AoE Attack Sequence
	}

    // Private
    private void SetAoEObjects() {
		hitBoxDirector = GetNode<Area3D>("AoE-Hit-Box-Director");
		hitBoxShape = hitBoxDirector.GetNode<CollisionShape3D>("AoE-Hit-Box");
	}
}