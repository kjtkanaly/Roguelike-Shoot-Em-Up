using Godot;
using System;
using System.Collections.Generic;

public partial class AoEAttackDirector : RepetativeAttackDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    
    // Protected

    // Private
    private AoEData data = null;
    private int enemyCount = 0;
    Dictionary<int, SceneTreeTimer> enemyTimers = 
        new Dictionary<int, SceneTreeTimer>(); 
    // GetTree().CreateTimer(1.0f)

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        SetAoEObjects();
        SetCollisionMaskValues();    

        hitBoxDirector.AreaEntered += BeginAoEDamageSequence;
        hitBoxDirector.AreaExited += EndAoEDamageSequence;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void LoadAttackDataFile() {
        // Start here 7/10/2024
		data = (AoEData) GD.Load(dataPath);
	}

    public override AoEData GetAttackData() {
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
				 * ((AoEData) data).areaIncreaseStepSize;
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

    protected void BeginAoEDamageSequence(Area3D opposingArea) {
        GD.Print($"{opposingArea.Name} Entered AoE");

        InteractionDirector otherIteraction = opposingArea.GetNode<InteractionDirector>("..");
        TimeDelayedDamageSequence(otherIteraction);

        /*
		// Get the Attack Information
		RepetativeAttackData aoeData = (RepetativeAttackData) enemyArea.GetParent<AoEAttackDirector>().GetAttackData();
        */
	}

    protected void EndAoEDamageSequence(Area3D enemyArea) {
        GD.Print("Enemy Exited AoE");
    }

	protected void TimeDelayedDamageSequence(InteractionDirector otherIteraction) {
        // Take the Initial Damage
		otherIteraction.TickHealth(GetAttackData().damage);
        /*
		// Create the new AoE object
		ActiveAoE aoe = new ActiveAoE(this, attackData);

		// Start the AoE Damage Timer
		aoe.delayTimer.Start();

		// Setup the aoe to end once out of range
		hitBoxDir.AreaExited += aoe.Destroy;
        /**/
	}

    // Private
    private void SetAoEObjects() {
		hitBoxDirector = GetNode<Area3D>("Hit-Box-Director");
		hitBoxShape = hitBoxDirector.GetNode<CollisionShape3D>("Hit-Box-Shape");
	}
}