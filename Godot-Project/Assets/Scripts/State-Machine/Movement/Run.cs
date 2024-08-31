using Godot;
using System;

public partial class Run : State
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float speedModifier = 1.0f;

    // Protected

    // Private
    [Export] private State jumpState;
    [Export] private State fallState;
    private Vector2 lateralDirection;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    override public State ProcessInput(InputEvent inputEvent) {
        // Trigger Jump Logic here
        if (inputEvent.IsAction("Jump") && characterDir.IsOnFloor()) {
            return jumpState;
        }
        return null;
    }

    override public State ProcessPhysics(float delta) {
        // Get the character's directional inputs
        lateralDirection = GetLateralDirectionVector();

        // Initialize the character's current speed and the goal speed
        float currentSpeed = characterDir.Velocity.Z;
        float goalSpeed = 0;

        // If the lateral direction isn't a zero vector then update the goal 
        // speed and orientate the body
        if (lateralDirection.Length() != 0) {
            OrientateBody(lateralDirection);
            goalSpeed = characterDir.GetMovementData().speed * speedModifier;
        }

        // Move the character's current closer to the goal speed 
        currentSpeed = Mathf.MoveToward(
                currentSpeed, 
                goalSpeed, 
                characterDir.GetMovementData().acceleration * delta);
        
        // Calcualte the character's lateral 2-D Velocity 
        Vector2 lateralVelocity =  
                new Vector2(Mathf.Sin(characterDir.Rotation.Y), 
                            Mathf.Cos(characterDir.Rotation.Y))
                * currentSpeed;

        // Update the character body's Velocity
        characterDir.Velocity = new Vector3(
                lateralVelocity.X, 
                characterDir.Velocity.Y, 
                lateralVelocity.Y);

        // Check if the player is now character is now falling due to their movement
        if (!characterDir.IsOnFloor()) {
            return fallState;
        }

        characterDir.MoveAndSlide();
        return null;
    }

    // Protected

    // Private
    private Vector2 GetLateralDirectionVector() {
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

    private void OrientateBody(Vector2 lateralDirection) {
		if (lateralDirection != Vector2.Zero) {
			float angle = -1 * (lateralDirection.Angle() - Mathf.Pi/2);
			characterDir.Rotation = new Vector3(
                    characterDir.Rotation.X, 
                    angle, 
                    characterDir.Rotation.Z);
		}
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
