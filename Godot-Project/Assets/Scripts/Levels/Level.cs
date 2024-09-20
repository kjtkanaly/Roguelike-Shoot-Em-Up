using Godot;
using System;

public partial class Level : Node3D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float spawnRadius = 30f;
    public float spawnDelay = 2.5f;

    // Protected
    [Export] protected PackedScene[] enemies;
    [Export] protected Timer[] spawnTimers;
    protected PlayerStats playerStats;
    protected Main main;

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
        main = GetNode<Main>("/root/Main");

        // Start the Spawn Timers
        for (int i = 0; i < enemies.GetLength(0); i++)
        {
            spawnTimers[i].Start(spawnDelay);
        }
    }

    public override void _Process(double delta)
    {
        for (int i = 0; i < enemies.GetLength(0); i++) 
        {
            if (spawnTimers[i].TimeLeft <= 0) 
            {
                SpawnEnemies(i);
                spawnTimers[i].Start(spawnDelay);
            }
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void SpawnEnemies(int index) 
    {
        Node3D enemyInstance = (Node3D) enemies[index].Instantiate();
        GetTree().Root.AddChild(enemyInstance);

        // Get Random Position by getting a random angle and then setting
        // the position at a constant radius
        float angle = main.rng.RandfRange(0, 2 * Mathf.Pi);
        Vector2 pos = new Vector2(spawnRadius, 0.0f);
        pos = pos.Rotated(angle);

        GD.Print($"Angle: {angle} | Pos: {pos}\n");

        // Translate the position to be centered on the player's current location
        pos += playerStats.GetPlayerTopDownPosition();

        // Set the enemy instance's positions
        enemyInstance.Position = new Vector3(
            pos.X,
            enemyInstance.Position.Y,
            pos.Y
        );
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
