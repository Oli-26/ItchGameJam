using Godot;
using System;

public partial class Launch : Node3D
{
	public override void _Ready()
	{
		CallDeferred("Start");
	}

	public void Start()
	{
		var levelManager = GetTree().Root
			.GetNode<LevelManager>("./LevelManager");
		levelManager.SetActiveScene(Levels.Phase1Level2);
	}
}
