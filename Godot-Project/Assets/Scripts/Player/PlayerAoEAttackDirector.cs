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
        timer = GetAttackTimer();
		meshInstance = GetMeshInstance();
		MainRoot = GetTree().Root.GetChild(0);

        SetAoEObjects();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void SetData(AttackData dataVal) {
		data = (AreaOfEffectData) dataVal;
	}

    public override void SetVisuals() {
        meshInstance.Mesh = data.areaMesh;
	}

	public override void SetColliderInformation() {
        AoEHitBox.Shape = data.areaColliderShape;
	}

    public override void LevelUpAttack() {
		level += 1;

        Scale += new Vector3(1, 1, 1) 
				 * ((AreaOfEffectData) data).areaIncreaseStepSize;
	}

    // Protected
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