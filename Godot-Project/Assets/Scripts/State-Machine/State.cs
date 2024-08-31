using Godot;
using System;

public partial class State : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public String animationPath;
    public float moveSpeed;

    // Protected
    protected CharacterDirector characterDir;
    protected AnimationPlayer animationPlayer;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void Init(CharacterDirector characterDirRef, AnimationPlayer animationPlayerRef) {
        characterDir = characterDirRef;
        animationPlayer = animationPlayerRef;
    }

    virtual public void Enter() {
        if (!string.IsNullOrEmpty(animationPath)) {
            animationPlayer.Play(animationPath);
        }
        return;
    }

    virtual public void Exit() {
        return;
    }

    virtual public State ProcessInput(InputEvent inputEvent) {
        return null;
    }

    virtual public State ProcessPhysics(float delta) {
        return null;
    }

    // Protected
    protected bool IsMovingLaterally() {
        if (Input.IsActionPressed("Left") 
            || Input.IsActionPressed("Right")
            || Input.IsActionPressed("Down")
            || Input.IsActionPressed("Up")) {
            return true;
        }
        return false;

        
    }

    protected Vector2 GetLateralDirectionVector() {
        Vector2 direction = new Vector2();
        switch (characterDir.charactertype) {
            case CharacterDirector.CharacterType.Player:
                direction = Input.GetVector("Left", "Right", "Up", "Down");
                break;
            case CharacterDirector.CharacterType.Ally:
                break;
            case CharacterDirector.CharacterType.Enemy:
                break;
            default:
                break;
        }
        return direction;
    }

    protected void OrientateBody() {
        Vector2 lateralDirection = GetLateralDirectionVector();
		if (lateralDirection != Vector2.Zero) {
			float angle = -1 * (lateralDirection.Angle() - Mathf.Pi/2);
			characterDir.Rotation = new Vector3(
                    characterDir.Rotation.X, 
                    angle, 
                    characterDir.Rotation.Z);
		}
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
