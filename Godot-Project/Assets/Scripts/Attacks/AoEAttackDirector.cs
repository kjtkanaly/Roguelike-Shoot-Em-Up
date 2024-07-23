using Godot;
using System;
using System.Diagnostics;

public partial class AoEAttackDirector : RepetativeAttackDirector
{
	//-------------------------------------------------------------------------
	// Game Componenets
	// Public
	
	// Protected

	// Private
	private AoEData data = null;

	//-------------------------------------------------------------------------
	// Game Events
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
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

	// Private
}
