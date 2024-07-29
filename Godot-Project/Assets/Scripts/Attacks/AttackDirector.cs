using Godot;
using System;
using System.Diagnostics;

public partial class AttackDirector : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public int level = 1;    
    [Export] public string dataPath;
    public string attackName = "";

    // Protected
    protected bool debug = true;
    protected Node mainRoot;
    protected Timer attackTimer = null;
    protected MeshInstance3D attackMesh = null;
    protected Area3D hitBoxDirector = null;
    protected CollisionShape3D hitBoxShape = null;
    [Export] protected int[] collisionMaskValues = {1};

    // Private 
    private AttackData data = null;
    [Export] private float yRotationAngleDegree = 0;
    private float yRotationAngle;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        LoadAttackDataFile();

        attackTimer = GetAttackTimerObject();
        attackMesh = GetMeshInstanceObject();
        hitBoxDirector = GetNode<Area3D>("Hit-Box-Director");
		hitBoxShape = hitBoxDirector.GetNode<CollisionShape3D>("Hit-Box-Shape");
        mainRoot = GetTree().Root.GetChild(0);

        attackName = GetNode<Node3D>("..").Name;
        yRotationAngle = Mathf.DegToRad(yRotationAngleDegree);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 scale = Scale;
        GlobalRotation = new Vector3(0, yRotationAngle, 0);
        Scale = scale;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public Methods
    public virtual void LoadAttackDataFile() {
        data = (AttackData) GD.Load(dataPath);
    }

    public virtual AttackData GetAttackData() {
        return data;
    }

    public Timer GetAttackTimerObject() {
        foreach (Node node in GetChildren()) {
            if (node.Name == "Attack-Timer") {
                return (Timer) node;
            }
        }

        return null;
    }

    public MeshInstance3D GetMeshInstanceObject() {
        foreach (Node node in GetChildren()) {
            if (node.Name == "Attack-Mesh") {
                return (MeshInstance3D) node;
            }
        }

        return null;
    }

    public virtual void SetVisuals() {
        
    }

    public virtual void SetColliderInformation() {
        
    }

    public virtual void LevelUpAttack() {
        level += 1;
    }

    public virtual string GetAttackId() {
        return GetAttackData().id;
    }

    public virtual int GetAttackMaxLevel() {
        return GetAttackData().maxLevel;
    }

    public void EnableAttack() {
        ProcessMode = ProcessModeEnum.Inherit;
    }

    public void DisableAttack() {
        ProcessMode = ProcessModeEnum.Disabled;
    }

    // Protected
    protected virtual void UpdateTimerSettings() {
        attackTimer.Timeout += CallAttack;
        attackTimer.WaitTime = GetAttackData().delay;
    }

    protected void StartTimer() {
        attackTimer.Start();
    }

    protected virtual void CallAttack() {
        if (debug) {
            GD.Print($"Generic Attack:");
        }
    }

    protected virtual void SetCollisionMaskValues() {
        foreach (int val in collisionMaskValues) {
            hitBoxDirector.SetCollisionMaskValue(val, true);
        }
    }

    // Private Methods

    //-------------------------------------------------------------------------
    // Debug Methods
}

