using Godot;
using System;

public partial class AnimationController : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public string idleAnimationString;
    [Export] public string walkRunAnimationString;
    [Export] public string DeathAnimationString;
    [Export] public string meshInstPath;
    [Export] public string hurtAnimationTimerPath;
    [Export] public string characterShaderPath;

    // Protected
    protected AnimationPlayer animationPlyr;
    protected MovementDirector movementDir;
    protected InteractionDirector interactionDir;
    protected ShaderMaterial characterShader;
    protected Timer hurtAnimationTimer;    
    protected float hurtAnimationDelay = 0.2f;
    protected bool unskippableAnimation = false;

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        SetAnimationPlayer();
        SetMovementDirector();
        SetInteractionDirector();

        interactionDir.TookDamage += PlayHurtAnimation;
        interactionDir.HasDied += PlayDeathAnimation;
        
        // Create an instance of the shader
        characterShader = 
            (ShaderMaterial) ResourceLoader.Load(characterShaderPath).Duplicate();

        // Set the character Shader Material
        MeshInstance3D meshInst = GetNode<MeshInstance3D>(meshInstPath);
        meshInst.Mesh.SurfaceSetMaterial(0, characterShader);
        

        // Get the Hurt Animation Timer
        hurtAnimationTimer = GetNode<Timer>(hurtAnimationTimerPath);
        hurtAnimationTimer.Timeout += StopHurtAnimation;

        StartIdleAnimation();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (unskippableAnimation) {
            return;
        }

        if (GetMovementDirector().GetLateralVelocityMag() > 0.1f) {
            StartWalkRunAnimation();
        } else {
            StartIdleAnimation();
        }

    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void StartWalkRunAnimation() {
        if (animationPlyr.CurrentAnimation == walkRunAnimationString) {
            return;
        }

        animationPlyr.Play(walkRunAnimationString);
    }

    public void StartIdleAnimation() {
        if (animationPlyr.CurrentAnimation == idleAnimationString) {
            return;
        }

        animationPlyr.Play(idleAnimationString);
    }

    public virtual void PlayHurtAnimation(AttackData data) {
        characterShader.SetShaderParameter("Enabled", true);
        hurtAnimationTimer.Start(hurtAnimationDelay);
    }

    public void StopHurtAnimation() {
        characterShader.SetShaderParameter("Enabled", false);
    }

    public void PlayDeathAnimation() {
        unskippableAnimation = true;
        animationPlyr.Play(DeathAnimationString);
    }

    // Protected
    protected virtual void SetAnimationPlayer() {
        animationPlyr = GetNode<AnimationPlayer>("Animation_Player");
    }

    protected virtual void SetMovementDirector() {
        movementDir = GetNode<MovementDirector>("..");
    }

    protected virtual void SetInteractionDirector() {
        foreach (Node child in GetParent().GetChildren()) {
            if (!child.IsInGroup("Interaction")) {
                continue;
            }

            interactionDir = (InteractionDirector) child;
            return;
        }
    }

    protected virtual MovementDirector GetMovementDirector() {
        return movementDir;
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
